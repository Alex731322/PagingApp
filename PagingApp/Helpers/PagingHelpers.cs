using NLog;
using PagingApp.Models;
using System;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
//.............................

public static class PagingHelpers
{
    //private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
    
    // , Func<int, string> pageUrl
    
    public static MvcHtmlString PageLinks(this HtmlHelper html,
        PageInfo pageInfo)
    {
        Logger logger = LogManager.GetCurrentClassLogger();

        StringBuilder result = new StringBuilder();
        for (int i = 1; i <= pageInfo.TotalPages; i++)
        {
            
             
            TagBuilder tag = new TagBuilder("a");
            tag.MergeAttribute("href", "/Home/?page=" + i.ToString() + "&&count=" + pageInfo.PageSize.ToString() );
            tag.InnerHtml = i.ToString();
            // если текущая страница, то выделяем ее,
            // например, добавляя класс
            if (i == pageInfo.PageNumber)
            {
                tag.AddCssClass("selected");
                tag.AddCssClass("btn-primary");
            }
            tag.AddCssClass("btn btn-default");
            result.Append(tag.ToString());
           
        }


        logger.Debug(MvcHtmlString.Create(result.ToString()));
        return MvcHtmlString.Create(result.ToString());
  

    }

        public static HtmlString CreateList(this HtmlHelper html, string[] items)
        {
            string result = "<ul>";
            foreach (string item in items)
            {
                result = $"{result}<li>{item}</li>";
            }
            result = $"{result}</ul>";
            return new HtmlString(result);
        }

    public static MvcHtmlString PageCount(this HtmlHelper html,
       PageInfo pageInfo)
    {
        Logger logger = LogManager.GetCurrentClassLogger();

        StringBuilder result = new StringBuilder();
        for (int i = 1; i <= 3; i++)
        {
            TagBuilder tag = new TagBuilder("a");
            tag.MergeAttribute("href", "/Home/?count=" + i.ToString() + "&&page=1" );
            tag.InnerHtml = i.ToString() + "Count";
            // если текущая страница, то выделяем ее,
            // например, добавляя класс
            if (i == pageInfo.PageSize)
            {
                tag.AddCssClass("selected");
                tag.AddCssClass("btn-primary");
            }
            tag.AddCssClass("btn btn-default");
            result.Append(tag.ToString());

        }

        return MvcHtmlString.Create(result.ToString());
    }

    public static MvcHtmlString InputDbData(this HtmlHelper html,
     string[] masVar, PageInfo pageInfo,string[] valPhone, string[] name  )
    {
        Logger logger = LogManager.GetCurrentClassLogger();

        StringBuilder result = new StringBuilder();
        int lengthMas;
        lengthMas = masVar.Count();

        int count = pageInfo.PageSize;
        int page = pageInfo.PageNumber;

        TagBuilder tag = new TagBuilder("form");
        tag.MergeAttribute("method", "get");
        tag.MergeAttribute("action", "/Berkut");
        

        tag.InnerHtml = "<input type=\"submit\" value= \"Добавить\" class=\"btn btn-default\" />";


       
        tag.InnerHtml += $"<input type=\"text\" value=\"{count}\" name=\"counttr\" autocomplete=\"on | off\" hidden =\"true\" />";
        tag.InnerHtml += $"<input type=\"text\" value=\"{page}\" name=\"pagetr\" autocomplete=\"on | off\" hidden =\"true\" />";

        

        for (int i = 0; i < lengthMas; i++)
        {
            tag.InnerHtml += "<br />";
            tag.InnerHtml += $"<h2>{name[i]}</h2>";
            tag.InnerHtml += $"<input type=\"text\" value=\"{valPhone[i]}\" name=\"{masVar[i]}\" autocomplete=\"on | off\" />";
        
        }
       
        result.Append(tag.ToString());       
        logger.Debug(MvcHtmlString.Create(result.ToString()));
        return MvcHtmlString.Create(result.ToString());
    }


    public static MvcHtmlString DeleteDbData(this HtmlHelper html,
    PageInfo pageInfo)
    {
        

        StringBuilder result = new StringBuilder();

        int count = pageInfo.PageSize;
       

        TagBuilder tag = new TagBuilder("form");
        tag.MergeAttribute("method", "get");
        tag.MergeAttribute("action", "/Berkut/DeleteDbdata");


        tag.InnerHtml += $"<input type=\"text\" value=\"{count}\" name=\"counttr\" autocomplete=\"on | off\" hidden =\"false\" />";
      
        tag.InnerHtml += "<input type=\"submit\" value= \"Удалить\" class=\"btn btn-default\" />";       
        tag.InnerHtml += "<br />";
        tag.InnerHtml += $"<input type=\"text\" name=\"Id\" autocomplete=\"on | off\" />";
        

        result.Append(tag.ToString());

        
        return MvcHtmlString.Create(result.ToString());


    }
    public static MvcHtmlString EditDbData(this HtmlHelper html,
    PageInfo pageInfo)
    {


        StringBuilder result = new StringBuilder();

        int count = pageInfo.PageSize;


        TagBuilder tag = new TagBuilder("form");
        tag.MergeAttribute("method", "get");
        tag.MergeAttribute("action", "/Berkut/EditDbData");


        tag.InnerHtml += $"<input type=\"text\" value=\"{count}\" name=\"counttr\" autocomplete=\"on | off\" hidden =\"false\" />";

        tag.InnerHtml += "<input type=\"submit\" value= \"Исправить\" class=\"btn btn-default\" />";
        
        tag.InnerHtml += "<br />";
        tag.InnerHtml += $"<input type=\"text\" name=\"Id\" autocomplete=\"on | off\" />";
       
        tag.InnerHtml += "<br />";
        tag.InnerHtml += $"<input type=\"text\" name=\"Model\" autocomplete=\"on | off\" />";
        
        tag.InnerHtml += "<br />";
        tag.InnerHtml += $"<input type=\"text\" name=\"Producer\" autocomplete=\"on | off\" />";


        result.Append(tag.ToString());


        return MvcHtmlString.Create(result.ToString());


    }


}