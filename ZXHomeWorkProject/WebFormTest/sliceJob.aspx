<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="sliceJob.aspx.cs" Inherits="WebFormTest.sliceJob" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <span id="cpmpareText" style="font-size: 24px"></span>
        </div>
        <button onclick="login()">点我登陆</button>
    </form>
    <script src="scripts/jquery-1.11.0.min.js"></script>
    <script type="text/javascript">
        //sliceJob();
        function sliceJob() {
            for (var i = 0; i < 100000; i++) {
                //$("#cpmpareText").html(i);

                var intv = setInterval(function () {
                    $("#cpmpareText").html(i);
                }, 50);
            }
            window.clearInterval(intv);


            //var num = (100000 / 100) + 1;//把任务数据划分为100份。
            //var portion = 100000;//每份有10万个数字。
            //var addition = 0;//这里用来保存最后的结果。一开始是0；
            //var intv = setInterval(function () {
            //    if (num--) {
            //        //然后每一份结果。
            //        additoin += every;
            //    } else {
            //        //计算最后一份，然后输出结果。
            //        alert('最终结果是:', addition);
            //        window.clearInterval(intv);
            //    }
            //}, 50);
        }
        function login() {
            var data = { Loginname: "wangya", Loginpwd: "1", Verifycode: "" };
            var json = JSON.stringify(data);
            var encrypted = encrypt.encrypt(json);
            json = encodeURI(encrypted).replace(/\+/g, '%2B');

            $.ajax({
                url: "http://10.64.0.248/data/ashx/system/LoginServer.ashx",
                data: { submitData: json },
                type: "post",
                dataType: "json",
                success: function (res) {
                    var a = res;
                }
            });
        }
    </script>
</body>
</html>
