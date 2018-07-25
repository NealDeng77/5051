/// <reference path ="../scripts/jquery/index.d.ts"/>

function sayHello() {

    var com = document.getElementById("compiler");
    var att = com.attributes;
    var val = att.length; 

    const compiler = "abc"; //(document.getElementById("compiler").attributes;
    const framework = "xyz"; // (document.getElementById("framework") as HTMLInputElement).value;
    return `Hello from ${compiler} and ${framework}!`;
}

//let myUrl = URL.parse("http://www.typescriptlang.org");

interface IJsonDataSimulator {
    Error: string[];
    Msg: string[];
    data: number;
}

function DataLoad(data: IJsonDataSimulator) {
    alert("Data load: " +
        " Error: " + data.Error +
        " Msg: " + data.Msg +
        " Data: " +data.data);
}

class Greeter {
    greeting: string;
    constructor(message: string) {
        this.greeting = message;
    }
    greet() {
        return "Hello, " + this.greeting;
    }
}

let greeter = new Greeter("world");

let button = document.createElement('button');
button.textContent = "Say Hello";
button.onclick = function() {
    alert(greeter.greet());
}

document.body.appendChild(button);

//alert("123");
//let Mike = sayHello();

$.ajax("/Game/Simulation",
    {
        cache: false,
        dataType: 'json',
        type: 'POST'
    })
    .done(function (data: any) {
        console.log(data);
        DataLoad(<IJsonDataSimulator>data);
    });
