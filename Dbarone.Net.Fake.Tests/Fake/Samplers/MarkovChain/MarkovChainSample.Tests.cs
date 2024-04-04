using System.Collections.Generic;
using Xunit;
using System.Net.Http;
using System.Linq;

namespace Dbarone.Net.Fake.Tests;

public class MarkovChainSamplerTests
{
    [Fact]
    public void WilliamFakespearSampler()
    {
        // Project Gutenberg - Complete works of William Shakespeare
        // We'll process his entire work to create a word-based model.
        string url = "https://www.gutenberg.org/cache/epub/100/pg100.txt";
        string text = "";

        HttpClient httpClient = new HttpClient();
        text = httpClient.GetStringAsync(url).Result;

        MarkovChainTrainer trainer = new MarkovChainTrainer();

        // We need to tell trainer how to handle unwanted parts of the text.
        IncludeLineDelegate includeLine = (string line, int index, ref Dictionary<string, object> state) =>
        {
            int i = 0;
            if (index <= 80)
            {
                // file header information
                return false;
            }
            else if (line.Trim() == "Contents" || line.Trim() == "THE END")
            {
                state["InContents"] = true;
                return false;
            }
            else if (string.IsNullOrEmpty(line.Trim()))
            {
                // empty line
                return false;
            }
            else if (line.Trim().Equals(line.Trim().ToUpper()) && line.Reverse().ToList()[0] == '.')
            {
                // Named part / speaker
                return false;
            }
            else if (line.Trim().StartsWith('[') && line.Trim().EndsWith(']'))
            {
                // Stage direction
                return false;
            }
            else if (line.Trim().StartsWith("Dramatis Personæ"))
            {
                state["Dramatis Personæ"] = true;
                return false;
            }
            else if (line.ToUpper().StartsWith("SCENE I.") && state.ContainsKey("Dramatis Personæ") && (bool)state["Dramatis Personæ"] == true && line.Trim().Length > "SCENE I.".Length)
            {
                state["Dramatis Personæ"] = false;
                state["InContents"] = false;
                return false;
            }
            else if (line.ToUpper().StartsWith("SCENE") || line.ToUpper().StartsWith("ACT"))
            {
                return false;
            }
            else if (int.TryParse(line.Trim(), out i) == true)
            {
                // Chapter numbers
                return false;
            }
            else if (state.ContainsKey("InContents") && (bool)state["InContents"] == true)
            {
                return false;
            }
            else if (line.Equals(line.ToUpper()))
            {
                // ignore lines completely in upper case
                return false;
            }
            else
            {
                return true;
            }
        };

        // We need to instruct the trainer how to ignore certain content.
        ProcessLineDelegate processLine = (string line) =>
        {
            line = line.Trim();
            var start = line.IndexOf("[");
            var end = line.Reverse().ToList().IndexOf(']');
            if (start >= 0 && end >= 0)
            {
                // remove stage directions from line
                line = line.Substring(line.Length - end);
            }
            return line.Trim();

        };

        var configuration = new MarkovChainTrainerConfiguration
        {
            WordDelimiters = new string[] { " " },
            Order = 2,
            Level = MarkovChainLevel.Word,
            IncludeLine = includeLine,
            ProcessLine = processLine
        };

        // We train the system based on the corpus (entire works of William Shakespear)
        var model = trainer.Train(text, configuration);

        // Create a new sampler using the model created above.
        var markov = new MarkovChainSampler(model);

        // Create first 20 words of a new prose based on what Shakespear would write?!?
        List<string> results = new List<string>();
        for (int i = 0; i < 20; i++)
        {
            results.Add(markov.Next(i, null));
        }
        var b = results;
        var actual = string.Join(" ", results);
        Assert.Equal("From fairest creatures we desire to see me down to tame a tongue in your words show you to use", actual);
    }

    [Fact]
    public void LoremIpsumCharacterSampler()
    {

        // Creates a character-based Markov chain model for Lorem ipsum... corpus, then generates a new text using _new_ words that sound vaguely latin...
        var corpus = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";
        MarkovChainTrainer trainer = new MarkovChainTrainer();
        MarkovChainTrainerConfiguration configuration = new MarkovChainTrainerConfiguration
        {
            Order = 2,
            Level = MarkovChainLevel.Character
        };
        var model = trainer.Train(corpus, configuration);
        MarkovChainSampler sampler = new MarkovChainSampler(model);

        List<string> words = new List<string>();
        for (int i = 1; i < 10; i++)
        {
            words.Add(sampler.Next(i));
        }
        var actual = string.Join(" ", words);
        Assert.Equal("Lor ex sincilliquipsunt mod incit ulliquis modolore dolorunt enim", actual); // that looks like real latin!
    }

    [Fact]
    public void PostTownCharacterSampler()
    {
        var corpus = "Abbots Langley|Aberaeron|Aberaeron, Lampeter|Aberdare|Aberdeen|Aberdyfi|Aberfeldy|Abergavenny, Crickhowell|Abergele|Aberlour|Abertillery|Aberystwyth, Llanon, Llanrhystud|Abingdon|Aboyne|Accrington|Acharacle|Achnasheen|Addlestone|Airdrie|Alcester|Aldeburgh|Alderley Edge, Wilmslow|Aldershot|Alexandria, Arrochar|Alford|Alfreton|Alloa, Clackmannan|Alness|Alnwick, Bamburgh|Alresford|Alston|Alton|Altrincham|Alva|Ambleside|Amersham|Amlwch|Ammanford|Andover|Annan|Anstruther|Antrim|Appin|Appleby-in-Westmorland|Arbroath|Ardgay|Ardrossan|Arisaig|Arlesey|Armagh|Arthog|Arundel|Ascot|Ashbourne|Ashby-de-la-Zouch|Ashford|Ashington|Ashtead|Ashton-under-Lyne|Askam-in-Furness|Atherstone|Attleborough|Auchterarder|Augher|Aughnacloy|Aviemore|Avoch|Axbridge|Axminster|Aylesbury|Aylesbury, Princes Risborough|Aylesford|Aylesford, Snodland, West Malling|Ayr|Bacup|Badminton|Bagillt, Flint|Bagshot|Bakewell|Bala|Baldock|Ballachulish|Ballater|Ballindalloch|Ballycastle|Ballyclare|Ballymena|Ballymoney|Ballynahinch|Bamburgh|Bampton, Burford, Carterton|Banbridge|Banbury|Banchory|Banff|Bangor|Banstead|Banwell|Bargoed|Barking|Barmouth|Barnard Castle|Barnet|Barnetby|Barnoldswick|Barnsley|Barnstaple|Barrow-in-Furness|Barrow-in-Furness, Dalton-in-Furness|Barrow-upon-Humber|Barry|Barton-upon-Humber|Basildon|Basingstoke|Basingstoke, Whitchurch|Bath|Bathgate|Batley|Battle|Beaconsfield|Beaminster|Beauly|Beaumaris|Beaworthy|Beccles|Beckenham|Beckermet|Bedale, Hawes, Leyburn|Bedford|Bedlington|Bedworth|Beith|Belfast|Belfast, Crumlin|Belford|Bellshill|Belper|Belvedere|Bembridge|Benfleet|Berkeley|Berkhamsted|Berriedale|Berwick-upon-Tweed|Betchworth|Betchworth, Dorking|Betws-y-Coed|Beverley|Bewdley|Bexhill-on-Sea|Bexley|Bexleyheath|Bexleyheath, Welling|Bicester|Bideford|Biggar|Biggleswade|Billericay|Billingham|Billingshurst|Bilston|Bingley|Birchington|Birkenhead|Birmingham|Bishop Auckland|Bishops Castle|Bishop's Stortford|Bishopton|Blackburn|Blackpool|Blackwood|Blaenau Ffestiniog|Blairgowrie|Blakeney, Lydney|Blandford Forum|Blaydon-on-Tyne|Blyth|Boat of Garten|Bodmin|Bodorgan|Bognor Regis|Boldon Colliery|Bolton|Boncath|Bo'ness|Bonnybridge|Bonnyrigg|Bootle|Bordon|Borehamwood|Borth, Bow Street, Talybont|Boscastle|Boston|Bourne|Bourne End|Bournemouth|Brackley|Bracknell|Bradford|Bradford-on-Avon|Braintree|Brampton|Brandon|Braunton|Brechin|Brecon|Brentford|Brentwood|Bridge of Orchy|Bridge of Weir|Bridgend|Bridgnorth|Bridgwater|Bridlington|Bridport|Brierley Hill|Brigg|Brighouse|Brighton|Bristol|Brixham|Broadstairs|Broadstone|Broadway|Brockenhurst|Bromley|Bromley, Keston|Bromsgrove|Bromyard|Brora|Broseley|Brough|Broughton-in-Furness|Broxbourne|Broxbourne, Hoddesdon|Broxburn|Bruton|Brynteg|Buckfastleigh|Buckhurst Hill|Buckie|Buckingham|Buckley, Mold|Bucknell, Craven Arms, Lydbury North|Bude|Budleigh Salterton|Builth Wells|Bungay|Buntingford|Bures|Burgess Hill|Burnham-on-Crouch, Southminster|Burnham-on-Sea|Burnley|Burntisland|Burntwood|Burry Port|Burton-on-Trent|Bury|Bury St. Edmunds|Bushey|Bushmills|Buxton|Caernarfon|Caerphilly|Caersws, Llandinam|Cairndow|Caldicot|Caledon|Callander|Callington|Calne|Calstock, Gunnislake|Camberley|Camborne|Cambridge|Camelford|Campbeltown|Cannock|Canonbie|Canterbury|Canvey Island|Cardiff|Cardigan|Carlisle|Carluke|Carmarthen|Carnforth|Carnoustie|Carrbridge|Carrickfergus|Carshalton|Castle Cary|Castle Douglas|Castlederg|Castlewellan|Caterham, Whyteleafe|Catterick Garrison|Cemaes Bay|Chalfont St. Giles|Chard|Chatham|Chathill|Chatteris|Cheadle|Cheddar|Chelmsford|Cheltenham|Chepstow|Chertsey|Chesham|Chessington|Chester|Chester le Street|Chesterfield|Chichester|Chigwell|Chigwell, Woodford Green|Chinnor|Chippenham|Chipping Campden|Chipping Norton|Chislehurst|Choppington|Chorley|Christchurch|Chulmleigh|Church Stretton|Cinderford, Newnham, Westbury-on-Severn|Cirencester, Fairford, Lechlade|Clacton-on-Sea|Clarbeston Road|Cleator|Cleator Moor|Cleckheaton|Cleethorpes|Clevedon|Clitheroe|Clogher|Clydebank|Clynderwen|Coalville, Ibstock, Markfield|Coatbridge|Cobham|Cockburnspath|Cockermouth|Colchester|Coldstream, Cornhill-on-Tweed, Mindrum|Coleford|Coleraine|Colintraive|Colne|Colwyn Bay|Colyton|Congleton|Coniston|Conwy|Conwy, Llandudno Junction|Cookstown|Corbridge|Corby|Corrour|Corsham|Corsham, Chippenham|Corwen|Cottingham|Coulsdon|Coventry|Coventry, Kenilworth|Cowdenbeath, Kelty|Cowes|Cradley Heath|Craigavon|Cramlington|Cranbrook|Cranleigh|Crawley|Crediton|Crewe|Crewkerne|Crianlarich|Criccieth|Crickhowell|Crieff|Cromarty|Cromer|Crook|Crowborough|Crowthorne|Croydon|Crymych|Cullompton|Cumnock|Cupar|Cwmbran|Dagenham|Dalbeattie|Dalkeith|Dalmally|Dalry|Dalton-in-Furness|Dalwhinnie|Darlington|Dartford|Dartford, Swanscombe|Dartmouth|Darvel|Darwen|Daventry|Dawlish|Deal|Deeside|Delabole|Denbigh|Denny|Derby|Dereham|Devizes|Dewsbury|Didcot|Dinas Powys, Penarth|Dingwall|Diss|Diss, Eye|Dolgellau|Dollar|Dolwyddelan|Donaghadee|Doncaster|Dorchester|Dorking|Dornoch|Doune|Dover|Downham Market|Downpatrick|Driffield|Droitwich|Dromore|Dronfield|Drybrook, Longhope, Lydbrook, Mitcheldean, Ruardean|Dudley|Dukinfield|Dulas|Dulverton|Dumbarton|Dumfries|Dunbar|Dunbeath|Dunblane|Dundee|Dunfermline|Dunfermline, Inverkeithing|Dungannon|Dunkeld|Dunmow|Dunmow, Braintree|Dunoon|Duns|Dunstable|Durham|Durham, Stanley, Consett|Dursley, Wotton-under-Edge|Dyffryn Ardudwy|Dymock, Newent|Earlston|East Boldon|East Cowes|East Grinstead|East Linton|East Molesey, West Molesey|Eastbourne|Eastleigh|Ebbw Vale|Edenbridge|Edgware|Edinburgh|Edinburgh, Balerno, Currie, Juniper Green|Egham|Egremont|Elgin|Elland|Ellesmere|Ellesmere Port|Ellon|Ely|Emsworth|Enfield|Enniskillen|Epping|Epsom|Erith|Erskine|Esher|Etchingham|Evesham, Broadway|Exeter|Exmouth|Eye|Eyemouth|Fairbourne|Fakenham|Falkirk|Falmouth|Fareham|Faringdon|Farnborough|Farnborough, Camberley|Farnham|Faversham|Felixstowe|Feltham|Ferndale|Ferndown|Ferryhill|Ferryside, Kidwelly|Filey|Fishguard|Fivemiletown|Fleet|Fleetwood|Fochabers|Folkestone|Fordingbridge|Forest Row|Forfar, Kirriemuir|Forres|Forsinard|Fort Augustus|Fort William|Fortrose|Fowey|Fraserburgh|Freshwater|Frinton-on-Sea|Frizington|Frodsham|Frome|Gaerwen|Gainsborough|Gairloch|Galashiels|Galston|Garndolbenmaen|Garve|Gateshead|Gatwick, Horley|Gerrards Cross|Gillingham|Girvan|Glasgow|Glastonbury|Glenfinnan|Glenrothes|Glogue|Glossop|Gloucester|Godalming|Godstone|Golspie|Goodwick|Goole|Gordon|Gorebridge|Gosport, Lee-on-the-Solent|Gourock|Grangemouth|Grange-over-Sands|Grantham|Grantown-on-Spey|Gravesend|Grays|Great Missenden|Great Yarmouth|Greenford|Greenhithe|Greenock|Gretna|Grimsby|Guildford|Guisborough|Gullane|Haddington|Hailsham|Halesowen|Halesworth|Halifax|Halifax, Elland|Halkirk|Halstead|Haltwhistle|Hamilton|Hampton|Harlech|Harleston|Harlow|Harpenden|Harrogate|Harrow|Hartfield|Hartlepool|Harwich|Hassocks|Hastings|Hatfield|Havant, Rowland's Castle|Haverfordwest|Haverhill|Hawick, Newcastleton|Hayes|Hayle|Hayling Island|Haywards Heath|Heanor|Heathfield|Hebburn|Hebden Bridge|Helensburgh|Helmsdale|Helston|Hemel Hempstead|Henfield|Hengoed|Henley-in-Arden|Henley-on-Thames|Henlow|Hereford|Heriot|Herne Bay|Hertford|Hessle|Hexham|Heywood|High Peak|High Wycombe|Highbridge|Hillsborough|Hinckley|Hindhead|Hindhead, Haslemere|Hinton St. George|Hitchin|Hitchin, Letchworth Garden City|Hockley|Holmfirth|Holmrook|Holsworthy|Holt|Holyhead|Holywell|Holywood|Honiton|Hook|Hope Valley|Horncastle|Hornchurch|Hornsea|Horsham|Houghton le Spring|Hounslow|Hove|Huddersfield|Hull|Humbie|Hungerford|Hunstanton|Huntingdon|Huntly|Hyde|Hythe|Ilford|Ilfracombe, Woolacombe|Ilkeston|Ilkley|Ilminster|Immingham|Ingatestone|Innerleithen|Insch|Inveraray|Invergarry|Invergordon|Inverness|Inverurie|Ipswich|Irvine|Isle of Arran|Isle of Barra|Isle of Benbecula|Isle of Bute|Isle of Coll|Isle of Colonsay|Isle of Cumbrae|Isle of Eigg|Isle of Gigha|Isle of Harris|Isle of Iona|Isle of Islay|Isle of Jura|Isle of Lewis|Isle of Mull|Isle of North Uist|Isle of Rum|Isle of Scalpay|Isle of Skye|Isle of South Uist|Isle of Tiree|Isles of Scilly|Isleworth|Iver|Ivybridge|Jarrow|Jedburgh|Johnstone|Keighley|Keith|Kelso|Kendal|Kenley, Purley|Keswick|Kettering|Kidderminster|Kidlington|Kilbirnie|Kilgetty|Killin|Kilmacolm|Kilmarnock|Kilwinning|Kinbrace|Kings Langley|Kings Langley, Watford|King's Lynn|Kingsbridge|Kingston upon Thames|Kingswinford|Kington|Kingussie|Kinlochleven|Kinross|Kirkby Stephen|Kirkby-in-Furness|Kirkcaldy|Kirkcudbright|Kirkliston|Kirknewton|Kirkwall|Knaresborough|Knebworth|Knighton|Knottingley|Knutsford|Kyle|Lairg|Lanark|Lancaster|Lancing|Langholm|Langport|Larbert|Largs|Larkhall|Larne|Lasswade|Latheron|Lauder|Launceston|Laurencekirk|Leamington Spa|Leatherhead|Ledbury|Leeds|Leek|Leicester|Leigh|Leigh-on-Sea|Leighton Buzzard|Leiston|Leominster|Leven|Lewes|Leyland|Lichfield|Lifton|Lightwater|Limavady|Lincoln|Lingfield|Linlithgow|Liphook|Lisburn|Liskeard|Liss|Littleborough|Littlehampton|Liverpool|Liverpool, Bootle|Liversedge|Liversedge, Heckmondwike|Livingston|Llanarth|Llanbedr|Llanbedrgoch|Llanbrynmair|Llandeilo, Llangadog, Llanwrda|Llandovery|Llandrindod Wells|Llandudno|Llandysul|Llanelli|Llanerchymedd|Llanfairfechan|Llanfairpwllgwyngyll|Llanfechain, Llanfyllin, Llansantffraid, Llanymynech, Meifod|Llanfyrnach|Llangammarch Wells|Llangollen|Llanidloes|Llanrwst|Llantwit Major|Llantwit Major, Cowbridge|Llanwrtyd Wells|Llanybydder|Llwyngwril|Loanhead|Lochailort|Lochearnhead|Lochgelly|Lochgilphead|Lochwinnoch|Lockerbie|London|Londonderry|Longfield|Longniddry, Prestonpans|Looe|Lossiemouth|Lostwithiel|Loughborough|Loughton|Louth|Lowestoft|Ludlow|Luton|Lutterworth|Lybster|Lyme Regis|Lymington|Lymm|Lyndhurst|Lynmouth, Lynton|Lytham St. Annes|Mablethorpe|Macclesfield|Macduff|Machynlleth|Maesteg|Maghera|Magherafelt|Maidenhead|Maidstone|Maldon|Mallaig|Malmesbury|Malpas|Malton|Malvern|Manchester|Manchester, Salford|Manningtree|Mansfield|Marazion|March|Margate, Birchington|Marianglas|Market Drayton|Market Harborough|Market Rasen|Marlborough|Marlow|Martock|Maryport|Matlock|Mauchline|Maybole|Mayfield|Melksham|Melrose|Melton Constable|Melton Mowbray|Menai Bridge|Menstrie|Merriott|Merthyr Tydfil|Mexborough|Middlesbrough|Middlewich|Midhurst|Milford Haven|Millom|Milltimber|Milnthorpe|Milton Keynes|Minehead|Mirfield|Mitcham|Moelfre|Moffat|Monmouth|Montacute|Montgomery|Montrose|Moor Row|Morden|Morecambe|Moreton-in-Marsh|Morpeth|Motherwell|Mountain Ash|Much Hadham|Much Wenlock|Muir of Ord|Munlochy|Musselburgh|Nairn|Nantwich|Narberth|Neath|Nelson|Neston|Nethy Bridge|New Malden|New Milton|New Quay|New Romney|New Tredegar|Newark|Newbiggin-by-the-Sea|Newbridge|Newbury|Newcastle|Newcastle Emlyn|Newcastle upon Tyne|Newhaven|Newmarket|Newmilns|Newport|Newport Pagnell|Newport, Yarmouth|Newport-on-Tay, Tayport|Newquay|Newry|Newton Abbot|Newton Aycliffe|Newton Stewart|Newton-le-Willows|Newtonmore|Newtown|Newtownabbey|Newtownards|Normanton|Normanton, Castleford|North Berwick|North Ferriby|North Shields|North Tawton, Okehampton|Northallerton|Northampton|Northolt, Greenford|Northwich|Northwood|Norwich|Norwich, Dereham|Norwich, North Walsham|Norwich, Sheringham|Norwich, Wymondham|Nottingham|Nottingham, Sutton-in-Ashfield|Nuneaton|Oakham|Oban|Oldbury|Oldham|Olney|Omagh|Ongar|Orkney|Ormskirk|Orpington|Ossett|Oswestry|Otley|Ottery St. Mary|Oxford|Oxted|Padstow|Paignton|Paisley|Par|Pathhead|Peacehaven|Peebles|Pembroke|Pembroke, Pembroke Dock|Pencader|Penicuik|Penmaenmawr|Penrhyndeudraeth|Penrith|Penryn|Pentraeth|Pentre|Penysarn|Penzance|Perranporth|Pershore|Perth|Peterborough|Peterculter|Peterhead|Peterlee|Petersfield|Petworth|Pevensey|Pewsey|Pickering|Pinner|Pitlochry|Plockton|Plymouth|Polegate|Pontefract|Pontyclun|Pontypool|Pontypridd|Poole|Port Glasgow|Port Isaac|Port Talbot|Porth|Porthcawl|Porthmadog|Portland|Portree|Portrush|Portsmouth|Portstewart|Potters Bar|Poulton-le-Fylde|Prenton|Prescot|Prestatyn|Presteigne|Preston|Prestwick|Princes Risborough|Prudhoe|Pudsey|Pulborough|Purfleet|Pwllheli|Queenborough|Radlett|Radstock|Rainham|Ramsgate|Ravenglass|Rayleigh|Reading|Reading, Thatcham|Redcar|Redditch|Redhill|Redruth|Reigate|Renfrew|Retford|Rhayader|Rhosgoch|Rhosneigr|Rhosneigr, Llangefni|Richmond|Rickmansworth|Riding Mill|Ringwood|Ripley|Ripon|Robertsbridge|Rochdale|Rochdale, Littleborough|Rochester|Rochford|Rogart|Romford|Romney Marsh|Romsey|Rosewell|Roslin|Rossendale|Ross-on-Wye|Rotherham|Rowlands Gill|Rowley Regis|Roy Bridge|Royston|Rugby|Rugeley|Ruislip|Runcorn|Rushden|Ruthin|Ryde|Rye|Ryton|Saffron Walden|Salcombe|Salford|Salisbury|Saltash|Saltburn-by-the-Sea|Saltcoats|Sandbach|Sandhurst|Sandown, Shanklin|Sandringham|Sandwich|Sandy|Sanquhar|Saundersfoot|Sawbridgeworth|Saxmundham|Scarborough|Scunthorpe|Seaford|Seaham|Seahouses|Seascale|Seaton|Seaview|Sedbergh|Selby|Selkirk|Sevenoaks|Shaftesbury|Shanklin|Sheerness|Sheffield|Shefford|Shepperton|Shepton Mallet|Sherborne|Shetland|Shifnal|Shildon|Shipley|Shipston-on-Stour|Shipston-on-Stour, Stratford-upon-Avon|Shoreham-by-Sea|Shotts|Shrewsbury|Sidcup|Sidmouth|Sittingbourne|Skegness|Skelmorlie|Skipton|Skipton, Settle|Sleaford|Slough|Smethwick|Solihull|Somerton|South Brent|South Croydon|South Molton|South Ockendon|South Petherton|South Queensferry|South Shields|Southall|Southall, Hayes|Southam|Southampton|Southampton, Lyndhurst|Southend-on-Sea|Southport|Southsea|Southwell|Southwold|Sowerby Bridge|Spalding|Spean Bridge|Spennymoor, Ferryhill|Spilsby|St. Agnes|St. Albans|St. Andrews|St. Asaph|St. Asaph, Rhyl|St. Austell|St. Bees|St. Columb|St. Helens|St. Ives|St. Leonards-on-Sea|St. Neots|Stafford|Staines-upon-Thames|Stalybridge|Stamford|Stanford-le-Hope|Stanley|Stanmore|Stansted|Stevenage|Stevenston|Steyning|Stirling|Stockbridge|Stockport|Stocksfield|Stockton-on-Tees|Stoke-on-Trent|Stoke-sub-Hamdon|Stone|Stonehaven|Stonehouse|Stornoway|Stourbridge|Stourport-on-Severn|Stowmarket|Strabane|Stranraer|Strathaven|Strathcarron|Strathdon|Strathpeffer|Street|Strome Ferry|Stromness|Stroud|Studley|Sturminster Newton|Sudbury|Sunbury-on-Thames|Sunderland|Surbiton|Sutton|Sutton Coldfield|Swadlincote|Swaffham|Swanage|Swanley|Swansea|Swindon|Tadcaster|Tadley|Tadworth|Tain|Talsarnau|Talybont|Tamworth|Tarbert|Tarporley|Taunton|Tavistock|Taynuilt|Teddington|Teignmouth|Telford|Templecombe|Tenbury Wells|Tenby|Tenterden|Tetbury|Tewkesbury|Thame|Thames Ditton|Thatcham|Thetford|Thirsk|Thornhill|Thornton Heath|Thornton-Cleveleys|Thurso|Tidworth|Tighnabruaich|Tilbury|Tillicoultry|Tintagel|Tipton|Tiverton|Todmorden|Tonbridge|Tonypandy|Torpoint|Torquay|Torrington|Totland Bay|Totnes, South Brent|Towcester|Tranent|Tredegar|Trefriw|Tregaron, Ystrad Meurig|Treharris|Treorchy|Trimdon Station|Tring|Troon|Trowbridge|Truro|Tunbridge Wells|Tunbridge Wells, Wadhurst|Turriff|Twickenham|Ty Croes|Tyn-y-Gongl|Tywyn|Uckfield|Ulceby|Ullapool|Ulverston|Umberleigh|Upminster|Usk|Uttoxeter|Uxbridge|Ventnor|Verwood|Virginia Water|Wadebridge|Wadhurst|Wakefield|Walkerburn|Wallasey|Wallingford|Wallington|Wallsend|Walsall|Walsingham|Waltham Abbey|Waltham Cross|Walton-on-Thames|Walton-on-the-Naze|Wantage|Ware|Wareham|Warlingham|Warminster|Warrington|Warwick|Washington|Watchet|Waterlooville|Watford|Watlington|Wedmore|Wednesbury|Welling|Wellingborough|Wellington|Wells|Wells-next-the-Sea|Welshpool|Welwyn|Welwyn Garden City|Welwyn, Welwyn Garden City|Wembley|Wemyss Bay|West Bromwich|West Byfleet|West Calder|West Drayton|West Drayton, Uxbridge|West Kilbride|West Linton|West Malling|West Wickham|Westbury|Westcliff-on-Sea|Westcliff-on-Sea, Southend-on-Sea|Westerham|Westgate-on-Sea|Westhill|Weston-super-Mare|Wetherby|Weybridge|Weymouth|Whitby|Whitchurch|Whitehaven|Whitland|Whitley Bay|Whitstable|Wick|Wickford|Widnes|Wigan|Wigan, Skelmersdale|Wigston|Wigton|Willenhall|Wimborne|Wincanton, Bruton|Winchelsea|Winchester|Windermere|Windlesham|Windsor|Wingate|Winkleigh|Winscombe|Winsford|Wirral|Wisbech|Wishaw|Witham|Withernsea|Witney|Woking|Wokingham|Wolverhampton|Wolverhampton, Willenhall|Woodbridge|Woodhall Spa|Woodstock|Wooler|Worcester|Worcester Park|Workington|Worksop|Worthing|Wotton-under-Edge|Wrexham|Wylam|Y Felinheli|Yarm|Yarmouth|Yateley|Yelverton|Yeovil|York";
        MarkovChainTrainer trainer = new MarkovChainTrainer();
        MarkovChainTrainerConfiguration configuration = new MarkovChainTrainerConfiguration
        {
            Order = 3,
            WordDelimiters = new string[] { "|" },
            Level = MarkovChainLevel.Character
        };
        var model = trainer.Train(corpus, configuration);
        MarkovChainSampler sampler = new MarkovChainSampler(model);

        List<string> words = new List<string>();
        for (int i = 1; i < 10; i++)
        {
            words.Add(sampler.Next(i));
        }
        var actual = string.Join(" ", words);
        Assert.Equal("Ardudno Chigwellingston Rowlanwells Borden Boot St Augh Ballynder-Edgwall Croydon Aboyne Thorley Kesterick Teigr", actual); // 10 town-sounding values.

        // Save the model
        var json = model.Serialise();
    }
}