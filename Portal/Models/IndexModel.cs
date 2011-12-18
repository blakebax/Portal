using System;
using Portal.Business;
using System.Collections.Generic;

/// <summary>
/// Represents an index page model.
/// </summary>
public class IndexModel
{
    /// <summary>
    /// A repsonse the a datastore query.
    /// </summary>
    public QueryResponse Response
    {
        get;
        set;
    }

    /// <summary>
    /// The Paging/Sorting options.
    /// </summary>
    public PageOptions PageOptions
    {
        get;
        set;
    }
}