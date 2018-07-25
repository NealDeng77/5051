/// <reference path ="../scripts/jquery/index.d.ts"/>
function sayHello() {
    var com = document.getElementById("compiler");
    var att = com.attributes;
    var val = att.length;
    const compiler = "abc"; //(document.getElementById("compiler").attributes;
    const framework = "xyz"; // (document.getElementById("framework") as HTMLInputElement).value;
    return `Hello from ${compiler} and ${framework}!`;
}
function DataLoad(data) {
    alert("Data load: " +
        " Error: " + data.Error +
        " Msg: " + data.Msg +
        " Data: " + data.data);
}
class Greeter {
    constructor(message) {
        this.greeting = message;
    }
    greet() {
        return "Hello, " + this.greeting;
    }
}
let greeter = new Greeter("world");
let button = document.createElement('button');
button.textContent = "Say Hello";
button.onclick = function () {
    alert(greeter.greet());
};
document.body.appendChild(button);
//alert("123");
//let Mike = sayHello();
$.ajax("/Game/Simulation", {
    cache: false,
    dataType: 'json',
    type: 'POST'
})
    .done(function (data) {
    console.log(data);
    DataLoad(data);
});
//# sourceMappingURL=game.js.map