/// <reference path ="../scripts/jquery/index.d.ts"/>

var BaseContentURL = "/Content/shop/"; 


// Get the Overall Image Box size
var ImageBox = $("#ImageBox");
var ImageBoxPosition = ImageBox.position();
var ImageBoxLeft = ImageBoxPosition.left;
var ImageBoxTop = ImageBoxPosition.top;
var ImageBoxWidth = ImageBox.width();
var ImageBoxHeight = ImageBox.height();

var TruckPositionTop = 220;
var StartPositionLeft = -400;
var EndPositionLeft = ImageBoxWidth;
console.log(EndPositionLeft);

var TruckPositionLeft = 500;
var StartPositionTop = TruckPositionTop;
var EndPositionTop = TruckPositionTop;

// Cause Animation to Happen
MoveShopper();

// Animate the Person
function MoveShopper() {
    var Shopper = $("#PeopleBox");
    Shopper.hide();

    Shopper.show();

    // Put an image on the Shopper
    // TODO:  In the future make it random...
    var ShopperItem = $("#PeopleItem");
    ShopperItem.attr("src", BaseContentURL + "Person1.png");

    // Choose how long it will take to Cross the Screen.
    var Duration = 3000;

    // Choose ending position

    // Set the Timmer for Animation Checks
    var AnimationCount = 0;

    var id = setInterval(frame, 1000);
    function frame() {
        if (AnimationCount > 0) {
            clearInterval(id);
        } else {

            AnimationCount++;

            ResetShopper(Shopper);

            var ShopperPosition = Shopper.position();
            var props = { 'top': EndPositionTop, 'left': EndPositionLeft };
            var options = { 'duration': Duration };

            MoveShopper(Shopper, props, options);

            ResetShopper(Shopper);
        }
    }

    function ResetShopper(el: any) {
        el.css({ 'top': StartPositionTop});
        el.css({ 'left': StartPositionLeft});
    }

    function MoveShopper(el:any, properties:any, options:any) {
        $("#PeopleBox").fadeIn("slow");

        // Animate to the Truck Window
        var TruckPosition = { 'top': TruckPositionTop, 'left': TruckPositionLeft};
        var origPos = el.position();

        el.css({
            top: origPos.top,
            left: origPos.left
        }).animate(TruckPosition, options);

        // Shop at same place...
        var origPos = el.position();
        el.css({
            top: origPos.top,
            left: origPos.left
        }).animate(TruckPosition, options);

        // Animate Off Screen
        var origPos = el.position();
        el.css({
            top: origPos.top,
            left: origPos.left
        }).animate(properties, options);

        $("#PeopleBox").fadeOut("slow");
    }
}
