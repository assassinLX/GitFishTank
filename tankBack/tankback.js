var express = require("express");
var app = express();

var fs = require('fs');

var data = []
var idSeq = 0;

var writeDataTofile = function() {
    var temp = { data: data, idSeq: idSeq }
    var jsonStr = JSON.stringify(temp)
    fs.writeFileSync(__dirname + '/data.json', jsonStr, 'utf8')
}

var readFileToData = function() {
    console.log('readFileToData');
    var jsonStr = fs.readFileSync(__dirname + '/data.json', 'utf8')
    var temp = JSON.parse(jsonStr)
    data = temp.data
    idSeq = temp.idSeq
}

readFileToData()

var addItem = function(userName, userPassword) {
    for (var i = 0; i < data.length; i++) {
        data[i].id = (i + 1);
    }
    var id = ++idSeq;
    var task = { id: id, userName: userName, password: userPassword };
    data.push(task);
    idSeq = data.length;
    writeDataTofile();
    return id;
}


app.get('/', function(req, res) {
    res.send(data);
});

app.get('/add/:userName/:userPassword', (req, res) => {
    var id = addItem(req.params.userName, req.params.userPassword);
    res.send({ id: id });

})



var server = app.listen(8888, function() {
    console.log("server is running on http://localhost:8888");
})