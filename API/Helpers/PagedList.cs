﻿using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;

namespace API.Helpers;

public class PagedList<T> : List<T> // "Generic" or "Template"
{
    public PagedList(IEnumerable<T> items, int count, int pageNumber, int pageSize)
    {
        this.CurrentPage = pageNumber;
        this.TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        this.PageSize = pageSize;
        this.TotalCount = count;
        this.AddRange(items);
    }

    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }

    // Creates a page of items 
    public static async Task<PagedList<T>> CreateAsync(IQueryable<T> source,
        int pageNumber, int pageSize)
    {
        var count = await source.CountAsync(); // Number of items in query
        var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
        return new PagedList<T>(items, count, pageNumber, pageSize);
    }
}
