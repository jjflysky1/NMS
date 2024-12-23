$(document).ready(function () {
    $.ajax({
        type: "POST",
        url: 'main9.aspx/javascript2',
        data: "",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            //alert("success");
            var customers = response.d;
            var product = "";
            var productarray = "";
            var status = "";
            var statusarray = "";
            $(customers).each(function () {
                product += this.product;
                status += this.status + ',';
            });
            productarray = product.split(',');
            statusarray = status.split(',');

            const element = document.getElementById('div33');
            element.innerHTML = product

            var sppositions = localStorage.ppositions || "{}",
                ppositions = JSON.parse(sppositions);
            $.each(ppositions, function (id, pos) {
                $("#" + id).css(pos)
            })

            for (var i = 0; i < 100; i++) {
                $("#product" + i).draggable({
                    containment: "#pan",
                    scroll: false,
                    stop: function (event, ui) {
                        ppositions[this.id] = ui.position
                        localStorage.ppositions = JSON.stringify(ppositions)

                        //if (arr.length !== 0) {
                        //    window.location.reload();
                        //}

                        var output = JSON.parse(localStorage.getItem('vvalue') || "{}")
                        var output1 = JSON.parse(localStorage.getItem('vvalue1') || "{}")
                        var output2 = JSON.parse(localStorage.getItem('pport1') || "{}")
                        var output3 = JSON.parse(localStorage.getItem('pport2') || "{}")

                        //for (var i = 0; i < localStorage.getItem('vvalue').length; i++) {
                        //    myLine.hide();
                        //}
                        $('.leader-line').remove();
                        try {
                            for (var i = 0; i < localStorage.getItem('vvalue').length; i++) {
                                var str = $('#' + output1[i]).find('img').attr('src').indexOf('error');
                                var str2 = $('#' + output[i]).find('img').attr('src').indexOf('error');
                                //-1 : 정상
                                if (str != '-1' || str2 != '-1') {
                                    col = '#de0707'
                                }
                                else {
                                    col = '#20de07'
                                }

                                var myLine = new LeaderLine(
                                    document.getElementById(output[i]),
                                    document.getElementById(output1[i]),
                                    {
                                        dash: { animation: true },
                                        path: "grid",
                                        endPlug: 'behind',
                                        color: col,
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
            var arrpport = new Array();
            var arrpport2 = new Array();

            //var output = localStorage.getItem("vvalue");
            //var output1 = localStorage.getItem("vvalue1");
            var output = JSON.parse(localStorage.getItem('vvalue') || "{}")
            var output1 = JSON.parse(localStorage.getItem('vvalue1') || "{}")
            var output2 = JSON.parse(localStorage.getItem('pport1') || "{}")
            var output3 = JSON.parse(localStorage.getItem('pport2') || "{}")

            try {
                for (var i = 0; i < localStorage.getItem('vvalue').length; i++) {
                    var str = $('#' + output1[i]).find('img').attr('src').indexOf('error');
                    var str2 = $('#' + output[i]).find('img').attr('src').indexOf('error');
                    //-1 : 정상
                    if (str != '-1' || str2 != '-1') {
                        col = '#de0707'
                    }
                    else {
                        col = '#20de07'
                    }

                    var myLine = new LeaderLine(
                        document.getElementById(output[i]),
                        document.getElementById(output1[i]),
                        {
                            dash: { animation: true },
                            //size:3,
                            path: "grid",
                            endPlug: 'behind',
                            color: col,
                            //startPlug:'arrow1',
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
                    if (arrpport.indexOf(output2[i]) !== null) {
                        arrpport.push(output2[i]);
                    }
                    if (arrpport2.indexOf(output3[i]) !== null) {
                        arrpport2.push(output3[i]);
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
                    arrpport.push(three);
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
                    arrpport2.push(four);
                }
                //alert("id : " + one + " // " + "id : " + two);
                var str = $('#' + one).find('img').attr('src').indexOf('error');
                var str2 = $('#' + two).find('img').attr('src').indexOf('error');
                //-1 : 정상
                if (str != '-1' || str2 != '-1') {
                    col = '#de0707'
                }
                else {
                    col = '#20de07'
                }
                var myLine = new LeaderLine(
                    document.getElementById(one),
                    document.getElementById(two),
                    {
                        dash: { animation: true },
                        path: "grid",
                        endPlug: 'behind',
                        color: col,
                        //size : 5
                        startLabel: three,
                        endLabel: four,
                    }

                );

                localStorage.setItem("vvalue", JSON.stringify(arr));
                localStorage.setItem("vvalue1", JSON.stringify(arr2));
                localStorage.setItem("pport1", JSON.stringify(arrpport));
                localStorage.setItem("pport2", JSON.stringify(arrpport2));

                //localStorage.setItem("vvalue", one);
                //localStorage.setItem("vvalue1", two);

                //alert(localStorage.getItem('vvalue'));
                //alert(localStorage.getItem('vvalue1'));
                //alert(localStorage.getItem('pport1'));
                //alert(localStorage.getItem('pport2'));
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
        url: 'main9.aspx/javascript2',
        data: "",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            //alert("success");
            var customers = response.d;
            var product = "";
            var productarray = "";
            var status = "";
            var statusarray = "";
            $(customers).each(function () {
                product += this.product;
                status += this.status + ',';
            });
            productarray = product.split(',');
            statusarray = status.split(',');

            const element = document.getElementById('div33');
            //element.innerHTML = "";
            element.innerHTML = product;

            var sppositions = localStorage.ppositions || "{}",
                ppositions = JSON.parse(sppositions);
            $.each(ppositions, function (id, pos) {
                $("#" + id).css(pos)
            })

            for (var i = 0; i < 100; i++) {
                $("#product" + i).draggable({
                    containment: "#pan",
                    scroll: false,
                    stop: function (event, ui) {
                        ppositions[this.id] = ui.position
                        localStorage.ppositions = JSON.stringify(ppositions)

                        var output = JSON.parse(localStorage.getItem('vvalue') || "{}")
                        var output1 = JSON.parse(localStorage.getItem('vvalue1') || "{}")
                        var output2 = JSON.parse(localStorage.getItem('pport1') || "{}")
                        var output3 = JSON.parse(localStorage.getItem('pport2') || "{}")

                        $('.leader-line').remove();
                        try {
                            for (var i = 0; i < localStorage.getItem('vvalue').length; i++) {
                                var str = $('#' + output1[i]).find('img').attr('src').indexOf('error');
                                var str2 = $('#' + output[i]).find('img').attr('src').indexOf('error');
                                //-1 : 정상
                                if (str != '-1' || str2 != '-1') {
                                    col = '#de0707'
                                }
                                else {
                                    col = '#20de07'
                                }

                                myLine = new LeaderLine(
                                    document.getElementById(output[i]),
                                    document.getElementById(output1[i]),
                                    {
                                        dash: { animation: true },
                                        path: "grid",
                                        endPlug: 'behind',
                                        color: col,
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
            var arrpport = new Array();
            var arrpport2 = new Array();

            //var output = localStorage.getItem("vvalue");
            //var output1 = localStorage.getItem("vvalue1");
            var output = JSON.parse(localStorage.getItem('vvalue') || "{}")
            var output1 = JSON.parse(localStorage.getItem('vvalue1') || "{}")
            var output2 = JSON.parse(localStorage.getItem('pport1') || "{}")
            var output3 = JSON.parse(localStorage.getItem('pport2') || "{}")

            $('.leader-line').remove();
            try {
                for (var i = 0; i < localStorage.getItem('vvalue').length; i++) {
                    var str = $('#' + output1[i]).find('img').attr('src').indexOf('error');
                    var str2 = $('#' + output[i]).find('img').attr('src').indexOf('error');
                    //-1 : 정상
                    if (str != '-1' || str2 != '-1') {
                        col = '#de0707'
                    }
                    else {
                        col = '#20de07'
                    }

                    var myLine = new LeaderLine(
                        document.getElementById(output[i]),
                        document.getElementById(output1[i]),
                        {
                            dash: { animation: true },
                            path: "grid",
                            endPlug: 'behind',
                            color: col,
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
                    if (arrpport.indexOf(output2[i]) !== null) {
                        arrpport.push(output2[i]);
                    }
                    if (arrpport2.indexOf(output3[i]) !== null) {
                        arrpport2.push(output3[i]);
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
                    arrpport.push(three);
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
                    arrpport2.push(four);
                }
                //alert("id : " + one + " // " + "id : " + two);
                var str = $('#' + one).find('img').attr('src').indexOf('error');
                var str2 = $('#' + two).find('img').attr('src').indexOf('error');
                //-1 : 정상
                if (str != '-1' || str2 != '-1') {
                    col = '#de0707'
                }
                else {
                    col = '#20de07'
                }
                var myLine = new LeaderLine(
                    document.getElementById(one),
                    document.getElementById(two),
                    {
                        dash: { animation: true },
                        path: "grid",
                        endPlug: 'behind',
                        color: '#20de07',
                        //size : 5
                        startLabel: three,
                        endLabel: four,
                    }

                );

                localStorage.setItem("vvalue", JSON.stringify(arr));
                localStorage.setItem("vvalue1", JSON.stringify(arr2));
                localStorage.setItem("pport1", JSON.stringify(arrpport));
                localStorage.setItem("pport2", JSON.stringify(arrpport2));

                //localStorage.setItem("vvalue", one);
                //localStorage.setItem("vvalue1", two);

                //alert(localStorage.getItem('vvalue'));
                //alert(localStorage.getItem('vvalue1'));
            });
        }
    });
}), 10000);

function RESET() {
    //localStorage.clear();

    localStorage.removeItem('ppositions');
    localStorage.removeItem('vvalue');
    localStorage.removeItem('vvalue1');
    localStorage.removeItem('pport1');
    localStorage.removeItem('pport2');
    window.location.reload();
}

function LINE() {
    localStorage.removeItem('vvalue');
    localStorage.removeItem('vvalue1');
    localStorage.removeItem('pport1');
    localStorage.removeItem('pport2');
    window.location.reload();
}

