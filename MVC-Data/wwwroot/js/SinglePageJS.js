//fetch all relevant elements
var elementBtnShowAll = document.getElementById("allPersonsBtn");
var elementBtnPersonDetails = document.getElementById("personDetailsBtn");
var elementBtnRemovePerson = document.getElementById("removePersonBtn");
var elementInputId = document.getElementById("inputPersonId");
var elementResultContainer = document.getElementById("PageResultContainer");


//add eventlisteners to the buttons
elementBtnShowAll.addEventListener("click", ajaxGetAllPersons);
elementBtnPersonDetails.addEventListener("click", ajaxPostPersonDetails);
elementBtnRemovePerson.addEventListener("click", ajaxPostRemovePerson);


//ajax methods
function ajaxGetAllPersons(event) {
    if (event != null) {            
        event.preventDefault();     //not sure if needed
    }
    $.get("SPA/DisplayPeople",
        function (data, status) {
            console.log("Data: " + data + "\nStatus: " + status);
            elementResultContainer.innerHTML = data;
        }
    );
}

function ajaxPostPersonDetails(event) {
    if (event != null) {
        event.preventDefault();  //maybe not needed
    }
    

    //console.log("in ajaxPostPersonDetails");
    //console.log("elementInputId ", elementInputId.value);
    
    $.post("SPA/Details",
    {
        id: elementInputId.value
    },
    function (data, status) {

        //partial view returns info if id is null or unknown 
        elementResultContainer.innerHTML = data;
    });
}

function ajaxPostRemovePerson(event) {
    if (event != null) {
        event.preventDefault(); //maybe not needed
    }
    //console.log("in ajaxPostRemovePerson");
    //console.log("elementInputId", elementInputId.value);
    
    $.post("SPA/RemovePerson",
        {
            id: elementInputId.value
        },
        function (data, status) {
            console.log("Data: " + data + "\nStatus: " + status);
            if (status == "success") {

                elementResultContainer.innerHTML = data;
            } 
        })
        .fail(function (jqXHR, textStatus, errorThrown) {
        console.log("jqXHR", jqXHR);
        console.log("textStatus", textStatus);
        console.log("errorThrown", errorThrown);
            if (jqXHR.status == 404) {
                elementResultContainer.innerHTML = jqXHR.responseText;
            } else {
                elementResultContainer.innerHTML = "<p>Problem to remove person.</p>";
            }
    });
    
}

function AjaxPersonDetails(event, id) {
    event.preventDefault();
    console.log("OnDetials Event: ", event);

    console.log("id:", id);

    var anchorUrl = event.target.href;
    console.log("anchorUrl", anchorUrl);

    $.post(anchorUrl,
        {
            id: id
        },
        function (data, status) {
            console.log("Data: " + data + "\nStatus: " + status);

            $("#p" + id).replaceWith(data);
        }
    );
}


function AjaxClosePersonDetails(event, id) {
    event.preventDefault();
    console.log("AjaxClosePersonDetails Event: ", event);

    console.log("id:", id);

    var anchorUrl = event.target.href;
    console.log("anchorUrl", anchorUrl);

    $.post(anchorUrl,
        {
            id: id
        },
        function (data, status) {
            console.log("Data: " + data + "\nStatus: " + status);

            $("#p" + id).replaceWith(data);
        }
    );

}



//show all people at start
ajaxGetAllPersons();