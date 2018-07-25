/// <reference path ="../scripts/jquery/index.d.ts"/>
var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : new P(function (resolve) { resolve(result.value); }).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
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
var RefreshRate = 1000;
// Game Update Timmer fires every RefeshRate
var GameUpdateTimer;
/*
 *
 * Data Functions
 *
 */
// Parses the Data Structure and returns the Iteration Number
function DataLoadIterationNumber(data) {
    console.log("Data load: " +
        " Error: " + data.Error +
        " Msg: " + data.Msg +
        " Data: " + data.Data);
    var IterationNumber = data.Data;
    return IterationNumber;
}
// Does a fetch to the server, and returns the Iteration Number
function GetSimulationIteration() {
    return __awaiter(this, void 0, void 0, function* () {
        $.ajax("/Game/Simulation", {
            cache: false,
            dataType: 'json',
            type: 'POST',
            success: function (data) {
                // Update the Global Server Iteration Number
                ServerIterationNumber = DataLoadIterationNumber(data);
            }
        })
            .fail(function () {
            console.log("IterationNumber error");
        });
    });
}
// Parses the Data Structure and returns the Iteration Number
function DataLoadGameResults(data) {
    console.log("Data load: " +
        " Error: " + data.Error +
        " Msg: " + data.Msg +
        " Data: " + data.Data);
    var IterationNumber = data.Data;
    return IterationNumber;
}
// Does a fetch to the server, and returns the Iteration Number
function GetGameResults() {
    $.ajax("/Game/Results/" + StudentId, {
        cache: false,
        dataType: 'json',
        type: 'POST',
        async: false,
        success: function (data) {
            // console.log(data);
            var IterationNumber = DataLoadIterationNumber(data);
            return IterationNumber;
        }
    })
        .fail(function () {
        console.log("Results error");
        return 0;
    });
    return 0;
}
// Get the Refresh rate for the page
// Returns the number of miliseconds to refresh
function GetRefreshRate() {
    // Set the Global Refresh rate
    RefreshRate = 1000;
}
// Refresh the Game
function RefreshGame() {
    return __awaiter(this, void 0, void 0, function* () {
        // Force a call to Simulation
        yield GetSimulationIteration();
        // Check if Game Version > current version, if so do update sequence
        if (ServerIterationNumber > CurrentIterationNumber) {
            // Get New Data
            GetGameResults();
            // Refresh Game Display
            RefreshGameDisplay();
            // Update Iteration Number
            CurrentIterationNumber = ServerIterationNumber;
        }
    });
}
// Refresh Game display
function RefreshGameDisplay() {
    // Use the current data structure
    // For all the elements in the Game, make a call and refresh them
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
// Get Refresh Rate
GetRefreshRate();
// Make Timmer to call refresh
setInterval(function () {
    RefreshGame();
}, RefreshRate);
//# sourceMappingURL=game.js.map