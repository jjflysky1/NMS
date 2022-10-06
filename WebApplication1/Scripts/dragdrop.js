
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
                product += this.product;
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
                        var output2 = JSON.parse(localStorage.getItem('port1') || "{}")
                        var output3 = JSON.parse(localStorage.getItem('port2') || "{}")

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
                                        startLabel: output2[i],
                                        endLabel: output3[i],
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
            var three = "";
            var four = "";
            var arr = new Array();
            var arr2 = new Array();
            var arrport = new Array();
            var arrport2 = new Array();

            //var output = localStorage.getItem("value");
            //var output1 = localStorage.getItem("value1");
            var output = JSON.parse(localStorage.getItem('value') || "{}")
            var output1 = JSON.parse(localStorage.getItem('value1') || "{}")
            var output2 = JSON.parse(localStorage.getItem('port1') || "{}")
            var output3 = JSON.parse(localStorage.getItem('port2') || "{}")


            try {
                for (var i = 0; i < localStorage.getItem('value').length; i++) {
                    var myLine = new LeaderLine(
                        document.getElementById(output[i]),
                        document.getElementById(output1[i]),
                        {
                            dash: { animation: true },
                            //size:3,
                            //path: "grid",
                            color: '#20de07',
                            //startPlug:'arrow1',
                            //endPlug: 'behind'
                            //size : 5
                            //startLabel: LeaderLine.captionLabel(output2[i], { color: 'white', outlineColor:'red' }),
                            startLabel: output2[i],
                            endLabel: output3[i],
                        }
                    );
                    if (arr.indexOf(output[i]) !== null) {
                        arr.push(output[i]);
                    }
                    if (arr2.indexOf(output1[i]) !== null) {
                        arr2.push(output1[i]);
                    }
                    if (arrport.indexOf(output2[i]) !== null) {
                        arrport.push(output2[i]);
                    }
                    if (arrport2.indexOf(output3[i]) !== null) {
                        arrport2.push(output3[i]);
                    }
                }
            }
            catch {

            }


            $(".product").click(function () {
                if (one.length != 0 && two.length != 0) {
                    one = "";
                    two = "";
                    three = "";
                    four = "";
                }
                if (one.length == 0 || three.length == 0) {
                    one = $(this).attr("id")
                    three = prompt("포트이름을 입력하세요.", "Text");
                    if (three === null) {
                        one = "";
                        return; //break out of the function early
                    }
                    alert("첫번재 장비 선택완료.")
                    arr.push(one);
                    arrport.push(three);
                }
                else if (two.length == 0 || four.length == 0) {
                    two = $(this).attr("id")
                    four = prompt("포트이름을 입력하세요.", "Text");
                    if (four === null) {
                        two = "";
                        return; //break out of the function early
                    }
                    alert("두번재 장비 선택완료.")
                    arr2.push(two);
                    arrport2.push(four);
                }
                //alert("id : " + one + " // " + "id : " + two);
                var myLine = new LeaderLine(
                    document.getElementById(one),
                    document.getElementById(two),
                    {
                        dash: { animation: true },
                        color: '#20de07',
                        //size : 5
                        startLabel: three,
                        endLabel: four,
                    }

                );

                localStorage.setItem("value", JSON.stringify(arr));
                localStorage.setItem("value1", JSON.stringify(arr2));
                localStorage.setItem("port1", JSON.stringify(arrport));
                localStorage.setItem("port2", JSON.stringify(arrport2));

                //localStorage.setItem("value", one);
                //localStorage.setItem("value1", two);

                //alert(localStorage.getItem('value'));
                //alert(localStorage.getItem('value1'));
                //alert(localStorage.getItem('port1'));
                //alert(localStorage.getItem('port2'));
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
                $("#product" + i).draggable({
                    containment: "#contain",
                    scroll: false,
                    stop: function (event, ui) {
                        positions[this.id] = ui.position
                        localStorage.positions = JSON.stringify(positions)

                        var output = JSON.parse(localStorage.getItem('value') || "{}")
                        var output1 = JSON.parse(localStorage.getItem('value1') || "{}")
                        var output2 = JSON.parse(localStorage.getItem('port1') || "{}")
                        var output3 = JSON.parse(localStorage.getItem('port2') || "{}")

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
                                        startLabel: output2[i],
                                        endLabel: output3[i]
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
            var one = "";
            var two = "";
            var three = "";
            var four = "";
            var arr = new Array();
            var arr2 = new Array();
            var arrport = new Array();
            var arrport2 = new Array();

            //var output = localStorage.getItem("value");
            //var output1 = localStorage.getItem("value1");
            var output = JSON.parse(localStorage.getItem('value') || "{}")
            var output1 = JSON.parse(localStorage.getItem('value1') || "{}")
            var output2 = JSON.parse(localStorage.getItem('port1') || "{}")
            var output3 = JSON.parse(localStorage.getItem('port2') || "{}")


            $('.leader-line').remove();
            try {
                for (var i = 0; i < localStorage.getItem('value').length; i++) {
                    var myLine = new LeaderLine(
                        document.getElementById(output[i]),
                        document.getElementById(output1[i]),
                        {
                            dash: { animation: true },
                            //path: "grid",
                            color: '#20de07',
                            //startPlug:'arrow1',
                            //endPlug: 'behind'
                            //size : 5
                            startLabel: output2[i],
                            endLabel: output3[i]
                        }
                    );
                    if (arr.indexOf(output[i]) !== null) {
                        arr.push(output[i]);
                    }
                    if (arr2.indexOf(output1[i]) !== null) {
                        arr2.push(output1[i]);
                    }
                    if (arrport.indexOf(output2[i]) !== null) {
                        arrport.push(output2[i]);
                    }
                    if (arrport2.indexOf(output3[i]) !== null) {
                        arrport2.push(output3[i]);
                    }
                }
            }
            catch {

            }


            $(".product").click(function () {
                if (one.length != 0 && two.length != 0) {
                    one = "";
                    two = "";
                    three = "";
                    four = "";
                }
                if (one.length == 0) {
                    one = $(this).attr("id")
                    three = prompt("포트이름을 입력하세요.", "Text");
                    if (three === null) {
                        one = "";
                        return; //break out of the function early
                    }
                    alert("첫번재 장비 선택완료.")
                    arr.push(one);
                    arrport.push(three);
                }
                else if (two.length == 0) {
                    two = $(this).attr("id")
                    four = prompt("포트이름을 입력하세요.", "Text");
                    if (four === null) {
                        two = "";
                        return; //break out of the function early
                    }
                    alert("두번재 장비 선택완료.")
                    arr2.push(two);
                    arrport2.push(four);
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
                localStorage.setItem("port1", JSON.stringify(arrport));
                localStorage.setItem("port2", JSON.stringify(arrport2));

                //localStorage.setItem("value", one);
                //localStorage.setItem("value1", two);

                //alert(localStorage.getItem('value'));
                //alert(localStorage.getItem('value1'));
            });
        }
    });
}), 10000);


function RESET() {
    localStorage.clear();
    window.location.reload();
}

function LINE() {
    localStorage.removeItem('value');
    localStorage.removeItem('value1');
    localStorage.removeItem('port1');
    localStorage.removeItem('port2');
    window.location.reload();
}

