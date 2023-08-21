using System;
using System.Collections.Generic;

namespace ProductAPI.Models;

public partial class CompanyInfo
{
    public int Cid { get; set; }

    public string Cname { get; set; } = null!;

    public virtual ICollection<ProductInfo> ProductInfos { get; set; } = new List<ProductInfo>();
}
