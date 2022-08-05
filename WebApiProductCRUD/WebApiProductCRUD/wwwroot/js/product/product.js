$('#btnSendObject').click(function (event) {
    event.preventDefault();
    var obj = createObject();
    await sendObject(obj);
})

async function sendObject(obj) {
    console.log(JSON.stringify(obj));
    $.post({
        url: '/Web/Product/Edit',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        data: JSON.stringify(dataToSend),
    })
}

function createObject() {
    var dataToSend = {
        'Id': $('#itemId').val(),
        'CreatedUtc': $('#itemDate').val(),
        'Name': $('#itemName').val(),
        'Price': $('#itemPrice').val(),
        'Stock': $('#itemStock').val()
    };
    return dataToSend;
}