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
                    }
                });
            }
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