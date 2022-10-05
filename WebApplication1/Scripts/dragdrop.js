
$(document).ready(function () {
    $.ajax({
        type: "POST",
        url: 'main8.aspx/javascript2',
        data: "",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            //alert("success");
            var customers = response.d;
            var product = "";
            var productarray = "";
            $(customers).each(function () {
                product += this.product ;
            });
            productarray = product.split(',');

            const element = document.getElementById('div33');
            element.innerHTML = product

            var sPositions = localStorage.positions || "{}",
                positions = JSON.parse(sPositions);
            $.each(positions, function (id, pos) {
                $("#" + id).css(pos)
            })

            for (var i = 0; i < 100; i++) {
                $("#product" + i).draggable({
                    containment: "#contain",
                    scroll: false,
                    stop: function (event, ui) {
                        positions[this.id] = ui.position
                        localStorage.positions = JSON.stringify(positions)

                        //if (arr.length !== 0) {
                        //    window.location.reload();
                        //}

                        var output = JSON.parse(localStorage.getItem('value') || "{}")
                        var output1 = JSON.parse(localStorage.getItem('value1') || "{}")
                        //for (var i = 0; i < localStorage.getItem('value').length; i++) {
                        //    myLine.hide();
                        //}
                        $('.leader-line').remove();
                        try {
                            for (var i = 0; i < localStorage.getItem('value').length; i++) {
                                myLine = new LeaderLine(
                                    document.getElementById(output[i]),
                                    document.getElementById(output1[i]),
                                    {
                                        dash: { animation: true },
                                        color: '#20de07',
                                        //size : 5
                                    }
                                );
                                
                               
                                
                            }
                        }
                        catch {

                        }
                        
                        
                    }
                });
            }

            var one = "";
            var two = "";
            var arr = new Array();
            var arr2 = new Array();
           
            //var output = localStorage.getItem("value");
            //var output1 = localStorage.getItem("value1");
            var output = JSON.parse(localStorage.getItem('value') || "{}")
            var output1 = JSON.parse(localStorage.getItem('value1') || "{}")

           
            try {
                for (var i = 0; i < localStorage.getItem('value').length; i++) {
                    var myLine = new LeaderLine(
                        document.getElementById(output[i]),
                        document.getElementById(output1[i]),
                        {
                            dash: { animation: true },
                            //path: "grid"
                            //color: '#20de07',
                            startPlug:'arrow1',
                            //endPlug: 'behind'
                            //size : 5
                            //startLabel: 'start label',
                            //endLabel: 'end label'
                        }
                    );
                    if (arr.indexOf(output[i]) !== null) {
                        arr.push(output[i]);
                    }
                    if (arr2.indexOf(output1[i]) !== null) {
                        arr2.push(output1[i]);
                    }
                }
            }
            catch {

            }

            $(".product").click(function () {
                if (one.length != 0 && two.length != 0) {
                    one = "";
                    two = "";
                }
                if (one.length == 0) {
                    one = $(this).attr("id")
                    alert("첫번재 장비 선택완료.")
                    arr.push(one);
                    
                }
                else if (two.length == 0) {
                    two = $(this).attr("id")
                    alert("두번재 장비 선택완료.")
                    arr2.push(two);
                    
                }    
                //alert("id : " + one + " // " + "id : " + two);
                var myLine = new LeaderLine(
                    document.getElementById(one),
                    document.getElementById(two),
                    {
                        dash: { animation: true },
                        color: '#20de07',
                        //size : 5
                    }

                );

                localStorage.setItem("value", JSON.stringify(arr));
                localStorage.setItem("value1", JSON.stringify(arr2));

                //localStorage.setItem("value", one);
                //localStorage.setItem("value1", two);

                //alert(localStorage.getItem('value'));
                //alert(localStorage.getItem('value1'));
                

            });
        },
        error: function (e) {
            alert("error" + e);
        }
    });
});



setInterval((function () {
    $.ajax({
        type: "POST",
        url: 'main8.aspx/javascript2',
        data: "",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            //alert("success");
            var customers = response.d;
            var product = "";
            var productarray = "";
            $(customers).each(function () {
                product += this.product;
            });
            productarray = product.split(',');

            const element = document.getElementById('div33');
            //element.innerHTML = "";
            element.innerHTML = product;

            var sPositions = localStorage.positions || "{}",
                positions = JSON.parse(sPositions);
            $.each(positions, function (id, pos) {
                $("#" + id).css(pos)
            })

            for (var i = 0; i < 100; i++) {
                $("#product"+i).draggable({
                    containment: "#contain",
                    scroll: false,
                    stop: function (event, ui) {
                        positions[this.id] = ui.position
                        localStorage.positions = JSON.stringify(positions)

                        var output = JSON.parse(localStorage.getItem('value') || "{}")
                        var output1 = JSON.parse(localStorage.getItem('value1') || "{}")
                    
                        $('.leader-line').remove();
                        try {
                            for (var i = 0; i < 100; i++) {
                                myLine = new LeaderLine(
                                    document.getElementById(output[i]),
                                    document.getElementById(output1[i]),
                                    {
                                        dash: { animation: true },
                                        color: '#20de07',
                                        //size : 5
                                    }
                                );

                                //if (arr.indexOf(output[i]) !== null) {
                                //    arr.push(output[i]);
                                //}
                                //if (arr2.indexOf(output1[i]) !== null) {
                                //    arr2.push(output1[i]);
                                //}

                            }
                        }
                        catch {

                        }
                    }
                });
            }
            //alert(serverip);
        },
        error: function (e) {
            alert("error" + e);
        }
    });
}), 10000);


function RESET() {
    localStorage.clear();
    window.location.reload();
}

function LINE() {
    //var myLine = new LeaderLine(
    //    document.getElementById('product0'),
    //    document.getElementById('product1'),
    //    {
    //        dash: { animation: true }
    //    }
    //);
    localStorage.removeItem('value');
    localStorage.removeItem('value1');
    window.location.reload();

    
    
}

