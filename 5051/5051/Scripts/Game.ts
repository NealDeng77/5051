/// <reference path ="../scripts/jquery/index.d.ts"/>

/*
 * 
 * Global Variables
 * 
 */

// Track the Current Iteration.  
// Use the Iteraction Number to determine, if new data refresh is needed, a change in number means yes, refresh data
// Start at -1, so first time run, always feteches data.
var CurrentIterationNumber = -1;

// Hold the Server's Iteration
var ServerIterationNumber = 0;

// The Student Id is stored in the Dom, need to fetch it on page load
var StudentId = $("#StudentId").val();

// Refresh rate is the rate to refresh the game in miliseconds
var ServerRefreshRate = 1000;

// Game Update Timmer fires every RefeshRate
var GameUpdateTimer;

// The Global Data for the Current ShopData
var ShopData = <iJsonDataResult>{};

/* 
 * 
 * Data Structures 
 * 
 */

// The Data structor for the IterationNumber Data set
interface IJsonDataIterationNumberHeader {
    Error: string[];
    Msg: string[];
    Data: number;
}

interface iJsonDataInventory {
    Name: string[];
    URI: string[];
}

// The result data
interface iJsonDataResult {
    Name: string;
    Truck: string;
    Topper: string;
    Trailer: string;
    Sign: string;
    Menu: string;
    Wheels: string;
    IterationNumber: number;
}

// The Data structor for the Result Data set Header
interface IJsonDataResultHeader {
    Error: string[];
    Msg: string[];
    Data: iJsonDataResult;
}

// The Data structor for the Result Data set Header
interface IJsonDataRefreshRateHeader {
    Error: string[];
    Msg: string[];
    Data: number;
}

/* 
 * 
 * Data Functions
 * 
 */

// Parses the Data Structure and returns the Iteration Number
function DataLoadIterationNumber(data: IJsonDataIterationNumberHeader): number {
    var IterationNumber = data.Data;
    return IterationNumber;
}

// Does a fetch to the server, and returns the Iteration Number
async function GetIterationNumber() {
    $.ajax("/Game/GetIterationNumber",
        {
            cache: false,
            dataType: 'json',
            type: 'POST',
            success: function (data: any) {
                // Update the Global Server Iteration Number
                ServerIterationNumber = DataLoadIterationNumber(<IJsonDataIterationNumberHeader>data);
            },
            error: function (data: any) {
                alert("Fail GetIterationNumber");
            }
        })
        .fail(function () {
            console.log("Error Get Iteration Number");
        });
}

// Parses the Data Structure and returns the Iteration Number
function DataLoadGameResults(data: IJsonDataResultHeader) {

    var result = <iJsonDataResult>data.Data;

    ShopData.Topper = result.Topper;
    ShopData.Menu = result.Menu;
    ShopData.Wheels = result.Wheels;
    ShopData.Sign = result.Sign;
    ShopData.Truck = result.Truck;
    ShopData.Trailer = result.Trailer;
    ServerIterationNumber = result.IterationNumber;
}

// Does a fetch to the server, and returns the Iteration Number
function GetGameResults(){

    var data = { "Id": StudentId.toString() };
    $.ajax(
        {
            url: "/Game/GetResults/",
            data: JSON.stringify(data),
            contentType: "application/json; charset=utf-8",
            cache: false,
            dataType: 'json',
            type: 'POST',
            async: false,
            success: function (data: any) {
                DataLoadGameResults(<IJsonDataResultHeader>data);
                return;
            },
            error: function (data: any) {
                alert("Fail GetResults");
            }
        })
        .fail(function () {
            console.log("Error GetResults");
            return;
        });

    return 0;
}

// Get the Refresh rate for the page
// Returns the number of miliseconds to refresh
function GetRefreshRate() {
    // Set the Global Refresh rate
    GetGameRefreshRate();
}

// Parses the Data Structure and returns the Iteration Number
function DataLoadRefreshRate(data: IJsonDataRefreshRateHeader): number {
    var rate = data.Data;
    return rate;
}

// Does a fetch to the server, and returns the Iteration Number
async function GetGameRefreshRate() {
    await $.ajax("/Game/GetRefreshRate/",
        {
            cache: false,
            dataType: 'json',
            type: 'POST',
            async: false,
            success: function (data: any) {
                ServerRefreshRate = DataLoadRefreshRate(<IJsonDataRefreshRateHeader>data);
            },
            error: function (data: any) {
                alert("Fail GetRefreshRate");
            }
        })
        .fail(function () {
            console.log("Error Get Refresh Rate");
        });
}

// Refresh the Game
async function RefreshGame() {

    // Force a call to Simulation
    await GetIterationNumber();

    // Check if Game Version > current version, if so do update sequence
    if (ServerIterationNumber > CurrentIterationNumber) {

        UpdateGame();
    }
}

function UpdateGame() {
    // Get New Data
    GetGameResults();

    // Refresh Game Display
    RefreshGameDisplay();

    // Update Iteration Number
    CurrentIterationNumber = ServerIterationNumber;
}

// Set the Default on Boot to draw, before the rest draws if no data exists
function SetDefaultShopData() {

    ShopData.Topper = "Topper0.png";
    ShopData.Menu = "Menu0.png";
    ShopData.Wheels = "Wheels0.png";
    ShopData.Sign = "Sign0.png";
    ShopData.Truck = "Truck0.png";
    ShopData.Trailer = "Trailer0.png";
}

/*
 * Application Starts Here
 * 
 * Load the Page
 * Initialize the Game
 * Do a fetch to the server to get IterationNumber, Refresh Rate
 * 
 * Set Refresh Rate on a Timmer
 * 
 * At the refresh go to the server and check for Iteration
 * 
 * If Current Iteration < Server Iteration Number, then refetch Data
 * 
 * Update Display for new Data
 * 
 */

// Set the Default Data for before the data loads from the server
SetDefaultShopData();

// Call for Refresh Game to get the Initial State
UpdateGame();

// Then start looping to refresh every RefreshRate Iteration

// Get Refresh Rate
GetRefreshRate();
console.log(ServerRefreshRate);

// Make Timmer to call refresh
setInterval(function () {
   RefreshGame();
}, ServerRefreshRate);

/*
 *
 * Game Layout Starts Here
 * 
 * 
 */

// Refresh Game display
function RefreshGameDisplay() {

    var BaseContentURL = "/Content/shop/"; 

    // Use the current data structure

    // For all the elements in the Game, make a call and refresh them

    // Show Iteration Number (debugging)
    $("#IterationNumber").text(CurrentIterationNumber);

    // Show Game Data
    $("#GameData").text("Game Data Goes Here");

    // Refesh Shop Skin
    $("#Truck").attr("src", BaseContentURL + ShopData.Truck);
    $("#Topper").attr("src", BaseContentURL + ShopData.Topper);
    $("#Menu").attr("src", BaseContentURL + ShopData.Menu);
    $("#Wheels").attr("src", BaseContentURL + ShopData.Wheels);
    $("#Sign").attr("src", BaseContentURL + ShopData.Sign);
    $("#Trailer").attr("src", BaseContentURL + ShopData.Trailer);
}
