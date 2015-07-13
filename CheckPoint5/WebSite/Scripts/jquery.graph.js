
google.load('visualization', '1', { packages: ['corechart', 'line'] });
google.setOnLoadCallback(drawBackgroundColor);

function drawBackgroundColor() {
    var data = new google.visualization.DataTable();
    data.addColumn('date', 'Time');
    data.addColumn('number', 'TotalSum');
    var json = $.ajax({
                    url: '/Order/GetDateAndCost',
                    dataType: 'json',
                    async: false,
                }).responseText;


    var pp=JSON.parse(json);
    for (var i = 0; i < pp.result.length; i++) 
    {
       data.addRows([[new Date(pp.result[i][1],pp.result[i][0]), pp.result[i][2]]]);
    }
    
    var options = {
        hAxis: {
            title: 'Time'
        },
        vAxis: {
            title: 'Popularity'
        },
        backgroundColor: '#f1f8e9'
    };

    var chart = new google.visualization.LineChart(document.getElementById('chart_div'));
    chart.draw(data, options);
}