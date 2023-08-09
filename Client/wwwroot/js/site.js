// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

//let konten1 = document.getElementById("konten1");
//let konten2 = document.getElementById("konten2");
//let konten3 = document.getElementById("konten3");

//const button1 = document.getElementById("b1");
//const button2 = document.getElementById("b2");
//const button3 = document.getElementById("b3");

//button1.addEventListener('click', () => {
//    konten1.style.backgroundColor = "blue";
    
//});
//button2.addEventListener('click', () => {
//    konten2.style.backgroundColor = "red";
    

//});
//button3.addEventListener('click', () => {
//    konten3.style.backgroundColor = "green";
    
//});

//const animals = [
//    { name: "dory", species: "fish", class: { name: "vertebrata" } },
//    { name: "tom", species: "cat", class: { name: "mamalia" } },
//    { name: "nemo", species: "fish", class: { name: "vertebrata" } },
//    { name: "umar", species: "cat", class: { name: "mamalia" } },
//    { name: "gary", species: "fish", class: { name: "human" } },
//]

//for (let i = 0; i < animals.length; i++) {
//    const animal = animals[i];
//    console.log("Animal name:", animal.name);
//    console.log("Species:", animal.species);

//    if (animal.species === "fish") {
//        animal.class.name = "non-mamalia";
//    }

//    console.log("Class:", animal.class.name);
//    console.log("------------------");

// let onlycat = [];

// for (let i = 0; i < animals.length; i++) {
//        if (animals[i].species === "cat") {
//            onlycat.push(animals[i]);
//        }
//  }

//    console.log(onlycat);
//}
https://pokeapi.co/api/v2/

//$.ajax({
//    url: "https://pokeapi.co/api/v2/pokemon"
//}).done((result) => {
//    let temp = "";

//    $.each(result.results, (key, val) => {
//        temp += `
//                <tr>
//                    <td>${key + 1}</td>
//                    <td>${val.name}</td>
//                    <td>${val.url}</td>
//                    <td><button onclick="detailSW('${val.url}')" data-bs-toggle="modal" data-bs-target="#modalSW" class="btn btn-primary">Detail</button></td>
//                </tr>
//            `;
//    })
//    $("#tbodySW").html(temp);
//});



//function detailSW(stringURL) {
//    $.ajax({
//        url: stringURL,
//        success: (result) => {
//            $('.modal-title').html(result.name);

//            let detailsHtml = `
//                <div class="d-flex align-items-center">
//                     <img src="${result.sprites.other.dream_world.front_default}" alt="${result.name}" class="img-fluid mr-5">
//                    <div>
//                        <h4>Attributes</h4>
//                        <p><strong>Height:</strong> ${result.height}</p>
//                        <p><strong>Weight:</strong> ${result.weight}</p>
//                        <p><strong>Types:</strong> <span class="badge bg-success">${result.types.map(type => type.type.name)}</span></p> 
//                        <p><strong>Abilities:</strong><span class="badge bg-danger">${result.abilities.map(ability => ability.ability.name)}</span> </p>
                      
//                        <p><strong>Status:</strong></p>
//                        ${result.stats.map(stat => `
//                            <p class="mb-0">${stat.stat.name}: ${stat.base_stat}</p>
//                            <div class="progress">
//                                <div class="progress-bar" role="progressbar" style="width: ${stat.base_stat}%" aria-valuenow="${stat.base_stat}" aria-valuemin="0" aria-valuemax="100"></div>
//                            </div>
//                        `).join('')}
//                    </div>
//                </div>
//            `;

//            $('.modal-body').html(detailsHtml);
//        }
//    });
//}

$(document).ready(function () {
    let table = new DataTable('#myTable', {
        ajax: {
            url: "https://localhost:7231/api/employees/",
            dataSrc: "data",
            dataType: "JSON"
        },
        columns: [
            { data: "nik" },
            { data: "firstName" },
            {
                data: 'birthDate',
                render: function (data, type, row) {
                    // Parse the date string to a JavaScript Date object
                    const dateObj = new Date(data);

                    // Format the date as "DD/MM/YYYY"
                    return dateObj.toLocaleDateString('en-GB');
                }
            },
            {
                data: "gender",
                render: function (data, type, row) {
                    if (data == 1) {
                        return "Male";
                    }
                    else {
                        return "Female";
                    }
                }
            },
            { data: "email" },
            { data: "phoneNumber" },
            {
                data: "",
                render: function (data, type, row) {
                    return `
                    <button onclick="deleteUser('${row.guid}')" class="btn btn-danger">Delete</button>
                    `
                }
            }
        ],
        dom: 'Bfrtip',
        buttons: [
            'copy', 'csv', 'excel', 'pdf', 'print'
        ]
    });
});

function insertUser() {
    var userObj = {
        firstName: $('#firstName').val(),
        lastName: $('#lastName').val(),
        birthDate: $('#birthDate').val(),
        gender: parseInt($('#gender').val()),
        email: $('#email').val(),
        phoneNumber: $('#phoneNumber').val(),
        hireDate: $('#hireDate').val()
    };

    $.ajax({
        url: "https://localhost:7231/api/employees", // Modify this to your server-side script
        method: 'POST',
        data: JSON.stringify(userObj),
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            // Handle success response
            alert('User inserted successfully:', response);
            location.reload();
        },
        error: function (error) {
            // Handle error response
            alert('Error inserting user:', error);
            console.log(userObj);
            console.log(error);
        }
    });
}

function deleteUser(userId) {
    const confirmation = confirm("Are you sure you want to delete this user?");
    if (!confirmation) {
        return; // If user cancels, exit the function
    }
    $.ajax({
        url: "https://localhost:7231/api/employees?guid=" + userId,
        method: "DELETE",
        success: function (response) {
            console.log("User deleted successfully:", response);
            alert("User delete succesfully");
            location.reload();
            // Perform any actions after successful deletion
        },
        error: function (error) {
            console.error("Error deleting user:", error);
            alert("Error when deleting data")
        }
    });
}

$(document).ready(function () {
    // Memuat data menggunakan Ajax
    $.ajax({
        url: "https://localhost:7231/api/employees/"
    }).done((result) => {
        // Menghitung jumlah gender 0 dan gender 1
        let gender0Count = 0;
        let gender1Count = 0;

        // Mengiterasi setiap baris data
        result.data.forEach(function (dataDetail) {
            if (dataDetail.gender === 0) {
                gender0Count++;
            } else if (dataDetail.gender === 1) {
                gender1Count++;
            }
        });

        // Menampilkan hasil perhitungan
        console.log("Total Gender 0 (Female):", gender0Count);
        console.log("Total Gender 1 (Male):", gender1Count);

        // Menginisialisasi Chart.js
        var xValues = ["Female", "Male"];
        var yValues = [gender0Count, gender1Count];
        var barColors = [
            "#b91d47",
            "#00aba9"
        ];

        new Chart("myChart1", {
            type: "pie",
            data: {
                labels: xValues,
                datasets: [{
                    backgroundColor: barColors,
                    data: yValues
                }]
            },
            options: {
                title: {
                    display: true,
                    text: "Employee Gender"
                }
            }
        });
    });
});

$(document).ready(function ageChart() {
    // Memuat data menggunakan Ajax
    $.ajax({
        url: "https://localhost:7231/api/employees/"
    }).done((result) => {
        // Process the fetched employee data here

        const currentDate = new Date(); // Current date
        const ageCounts = {};

        result.data.forEach(employee => {
            const birthDate = new Date(employee.birthDate);
            const age = currentDate.getFullYear() - birthDate.getFullYear();

            // Counting the occurrences of each age
            if (ageCounts[age]) {
                ageCounts[age]++;
            } else {
                ageCounts[age] = 1;
            }
        });

        var xValues = Object.keys(ageCounts); // Get unique ages
        var yValues = Object.values(ageCounts); // Get counts for each age
        var barColors = "#b91d47";

        new Chart("myChart2", {
            type: "bar",
            data: {
                labels: xValues,
                datasets: [{
                    backgroundColor: barColors,
                    data: yValues
                }]
            },
            options: {
                title: {
                    display: true,
                    text: "Employee Age Distribution"
                },
                scales: {
                    x: {
                        title: {
                            display: true,
                            text: "Age"
                        }
                    },
                    y: {
                        title: {
                            display: true,
                            text: "Count"
                        }
                    }
                }
            }
        });
    }).fail((error) => {
        console.error("Error fetching employee data:", error);
    });
});