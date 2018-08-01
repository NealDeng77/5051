/// <reference path ="../scripts/jquery/index.d.ts"/>

var BaseContentURL = "/Content/shop/"; 

// Get the Overall Image Box size
var ImageBox = $("#ImageBox");
var ImageBoxPosition = ImageBox.position();
var ImageBoxLeft = ImageBoxPosition.left;
var ImageBoxTop = ImageBoxPosition.top;
var ImageBoxWidth = ImageBox.width();
var ImageBoxHeight = ImageBox.height();

// Cause Animation to Happen
MoveShopper();

// Animate the Shop Worker
MoveWorker();

// Animate the Person
function MoveShopper() {
    var PeopleBox = $("#PeopleBox");
    var PeopleBoxPosition = PeopleBox.position();
    var PeopleBoxTop = PeopleBoxPosition.top;

    var TruckPositionTop = PeopleBoxTop;
    var StartPositionLeft = -400;
    var EndPositionLeft = ImageBoxWidth;

    var TruckPositionLeft = 500;
    var StartPositionTop = TruckPositionTop;
    var EndPositionTop = TruckPositionTop;

    var Shopper = $("#PeopleBox");
    Shopper.hide();

    Shopper.show();

    // Put an image on the Shopper
    // TODO:  In the future make it random...
    var ShopperItem = $("#PeopleItem");
    ShopperItem.attr("src", BaseContentURL + "People1.png");

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

            ResetObject(Shopper, StartPositionTop, StartPositionLeft);

            var ShopperPosition = Shopper.position();
            var props = { 'top': EndPositionTop, 'left': EndPositionLeft };
            var options = { 'duration': Duration };

            MoveShopper(Shopper, props, options);

            ResetObject(Shopper, StartPositionTop, StartPositionLeft);
        }
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

// Animate the Person
function MoveWorker() {
    var WorkerBox = $("#WorkerBox");
    var WorkerBoxPosition = WorkerBox.position();

    var WorkerBoxLeftItem = $("#WorkerBoxLeft");
    var WorkerBoxLeftPosition = WorkerBoxLeftItem.position();
    var WorkerBoxTop = WorkerBoxLeftPosition.top;
    var WorkerBoxLeft = WorkerBoxLeftPosition.left;
    var WorkerBoxRight = WorkerBoxLeftItem.width()+WorkerBoxLeft;

    var WorkerBoxMid = WorkerBoxLeft + ((WorkerBoxRight-WorkerBoxLeft)/2);

    var Worker = $("#WorkerBox");
    Worker.hide();

    Worker.show();

    // Put an image on the Worker
    // TODO:  In the future make it the Avatar of the Student...
    var WorkerItem = $("#WorkerItem");
    WorkerItem.attr("src", BaseContentURL + "Worker1.png");

    // Choose how long it will take to Cross the Screen.
    var Duration = 2000;

    // Choose ending position

    // Set the Timmer for Animation Checks
    var AnimationCount = 0;

    var id = setInterval(frame, 1000);
    function frame() {
        if (AnimationCount > 10) {
            clearInterval(id);
        } else {

            AnimationCount++;

            //ResetObject(Worker, WorkerBoxTop, WorkerBoxRight);

            var WorkerPosition = Worker.position();
            var options = { 'duration': Duration };

            var StartPosition = { 'top': WorkerBoxTop, 'left': WorkerBoxRight };
            var MidPosition = { 'top': WorkerBoxTop, 'left': WorkerBoxMid };
            var EndPosition = { 'top': WorkerBoxTop, 'left': WorkerBoxLeft };

            // Move worker back
            MoveObject(Worker, StartPosition, MidPosition, EndPosition, options);

            var StartPosition = { 'top': WorkerBoxTop, 'left': WorkerBoxLeft };
            var MidPosition = { 'top': WorkerBoxTop, 'left': WorkerBoxMid };
            var EndPosition = { 'top': WorkerBoxTop, 'left': WorkerBoxRight };

            MoveObject(Worker, StartPosition, MidPosition, EndPosition, options);

            //ResetObject(Worker, WorkerBoxTop, WorkerBoxRight);
        }
    }
}

function ResetObject(el: any, top: number, left: number) {
    el.fadeOut("fast");

    el.fadeIn("slow");

    el.css({ 'top': top });
    el.css({ 'left': left });


}

function MoveObject(el: any, StartPosition: any, MidPosition: any, EndPosition: any, options: any) {

    // Animate to the Truck Window
    var currentPos = el.position();
    el.css({
        top: currentPos.top,
        left: currentPos.left
    }).animate(MidPosition, options);

    // Shop at same place...
    currentPos = el.position();
    el.css({
        top: currentPos.top,
        left: currentPos.left
    }).animate(MidPosition, options);

    // Animate Off Screen
    currentPos = el.position();
    el.css({
        top: currentPos.top,
        left: currentPos.left
    }).animate(EndPosition, options);

}
