let table_question = $('#question-table').DataTable({
    retrieve: true,
    columnDefs: [
        {

            className: "text-center",
            'createdCell': function (td, cellData, rowData, row, col) {
                // this will give each cell an ID
                $(td).attr('id', 'ques-' + rowData[0]);
            },
            targets: 0,
        }
    ]
});

let table_answer = $('#answer-table').DataTable({
    retrieve: true,
    columnDefs: [
        {

            className: "text-center",
            'createdCell': function (td, cellData, rowData, row, col) {
                // this will give each cell an ID
                $(td).attr('id', 'anws-' + rowData[0] + '-ques-' + row);
            },
            targets: 0,
        }
    ]
});

$(document).ready(function () {
    displayTabs(true);
    setInterval(initQuestionTable(), 3000);
})


//Set value to table 
function initQuestionTable() {
    $.ajax({
        headers: {
            "Access-Control-Allow-Origin": "*",
        },
        type: "GET",
        contentType: "application/json",
        Credential: "include",
        url: "https://localhost:7099/api/Questions",
        xhrFields: {
            withCredentials: true,
        },
        async: false,
        error: function () {
            console.log("Error");
        },
        success: function (data) {

            table_question.clear().draw();

            for (var i = 0; i < data.length; i++) {
                table_question.row
                    .add([
                        data[i].question_id,
                        data[i].question_content,
                        "<td> <a id='update-product-btn' onclick='updateQuestion(" + data[i].question_id + ")' data-bs-toggle='modal' data-bs-target='#exampleModal'><i class='fa-solid fa-pen-to-square fa-lg'></i></a> <a id='detail-product-btn' onclick='showDetailQuestion(" + data[i].question_id + ")'><i class='fa-solid fa-circle-info fa-lg'></i></a></td>"
                    ])
                    .draw();
            }
        },
    });
}
function initAnswerTable(data) {
    for (var i = 0; i < data.length; i++) {
        table_answer.row
            .add([
                data[i].answer_id,
                data[i].answer_content,
                data[i].isCorrect,
                "<td> <a id='update-answer-btn' onclick='updateAnswer(" + data[i].answer_id + ")' data-bs-toggle='modal' data-bs-target='#exampleModal'><i class='fa-solid fa-pen-to-square fa-lg'></i></td>",
            ])
            .draw();
    }
}


$('#back-question-btn').click(function()
{
    displayTabs(true);
});
//----------------------------------------------------------------

// Showing tab question or answer
function displayTabs(isShowQuestion) {
    if (isShowQuestion) {
        $('#quesion-tabs').show();
        $('#answer-tabs').hide();
    }
    else {
        $('#quesion-tabs').hide();
        $('#answer-tabs').show();
    }
}


//----------------------------------------------------------------
// Showing all answer of the question
function showDetailQuestion(id) {
    $.ajax({
        headers: {
            "Access-Control-Allow-Origin": "*",
        },
        type: "GET",
        contentType: "application/json",
        Credential: "include",
        url: "https://localhost:7099/api/Answers/" + id,
        xhrFields: {
            withCredentials: true,
        },
        async: false,
        error: function () {
            console.log("Error");
        },
        success: function (data) {
            displayTabs(false);
            $('#header-answer').empty().text('All Answer Of Question Number: '+id);
            initAnswerTable(data);
        },
    });
}