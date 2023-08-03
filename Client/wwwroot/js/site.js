// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

let konten1 = document.getElementById("konten1");
let konten2 = document.getElementById("konten2");
let konten3 = document.getElementById("konten3");

const button1 = document.getElementById("b1");
const button2 = document.getElementById("b2");
const button3 = document.getElementById("b3");

button1.addEventListener('click', () => {
    konten1.style.backgroundColor = "blue";
    
});
button2.addEventListener('click', () => {
    konten2.style.backgroundColor = "red";
    

});
button3.addEventListener('click', () => {
    konten3.style.backgroundColor = "green";
    
});


