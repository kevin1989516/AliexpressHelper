<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="express.Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        body {
            font-size:12px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div>
                <table>
                    <tr>
                        <td>公司:</td>
                        <td><asp:TextBox ID="txtCompany" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>关键词:</td>
                        <td><asp:TextBox ID="txtKewWords" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td><asp:Button ID="btnSearch" runat="server" Text="搜索" OnClick="btnSearch_Click" /></td>
                    </tr>
                </table>
            </div>
            <asp:GridView ID="GridView1" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None"
                AutoGenerateColumns="False" OnRowDataBound="GridView1_RowDataBound">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            时间
                        </HeaderTemplate>
                        <ItemTemplate>
                            <%#DataBinder.Eval(Container.DataItem, "CreateTime","{0:yyyy-MM-dd}")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            关键词
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbKeyWordsId" Text='<%#DataBinder.Eval(Container.DataItem, "KeyWordsId")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            Listing排序
                        </HeaderTemplate>
                        <ItemTemplate>
                            <%#DataBinder.Eval(Container.DataItem, "ProductOrder")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            公司
                        </HeaderTemplate>
                        <ItemTemplate>
                            <%#DataBinder.Eval(Container.DataItem, "CompanyName")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            价格From
                        </HeaderTemplate>
                        <ItemTemplate>
                            <%#DataBinder.Eval(Container.DataItem, "PriceFrom")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            价格To
                        </HeaderTemplate>
                        <ItemTemplate>
                            <%#DataBinder.Eval(Container.DataItem, "PriceTo")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            公司
                        </HeaderTemplate>
                        <ItemTemplate>
                            <%#DataBinder.Eval(Container.DataItem, "ProductOrder")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            评论
                        </HeaderTemplate>
                        <ItemTemplate>
                            <%#DataBinder.Eval(Container.DataItem, "Feedback")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            订单量
                        </HeaderTemplate>
                        <ItemTemplate>
                            <%#DataBinder.Eval(Container.DataItem, "OrdersQty")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            产品链接
                        </HeaderTemplate>
                        <ItemTemplate>
                            <a href="<%# DataBinder.Eval(Container.DataItem, "ProudctLink") %>" target="_blank">url<a />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <EditRowStyle BackColor="#999999" />
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
            </asp:GridView>
        </div>
    </form>
</body>
</html>
