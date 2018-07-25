/// <reference path ="../scripts/jquery/index.d.ts"/>
/*
 *
 * Global Variables
 *
 */
// Track the Current Iteration.  
// Use the Iteraction Number to determine, if new data refresh is needed, a change in number means yes, refresh data
// Start at -1, so first time run, always feteches data.
var CurrentIteration = -1;
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
    $.ajax("/Game/Simulation", {
        cache: false,
        dataType: 'json',
        type: 'POST'
    })
        .done(function (data) {
        // console.log(data);
        var IterationNumber = DataLoadIterationNumber(data);
        return IterationNumber;
    });
    return 0;
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
        type: 'POST'
    })
        .done(function (data) {
        // console.log(data);
        var IterationNumber = DataLoadIterationNumber(data);
        return IterationNumber;
    });
    return 0;
}
// Get the Refresh rate for the page
// Returns the number of miliseconds to refresh
function GetRefreshRate() {
    return 1000;
}
// Refresh the Game
function RefreshGame() {
    // Force a call to Simulation
    var NewIteration = GetSimulationIteration();
    // Check if Game Version > current version, if so do update sequence
    if (NewIteration > CurrentIteration) {
        // Get New Data
        GetGameResults();
        // Refresh Game Display
        RefreshGameDisplay();
        // Update Iteration Number
        CurrentIteration = NewIteration;
    }
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
RefreshRate = GetRefreshRate();
// Make Timmer to call refresh
this.GameUpdateTimer = setTimeout(() => RefreshGame(), RefreshRate);
//# sourceMappingURL=game.js.map