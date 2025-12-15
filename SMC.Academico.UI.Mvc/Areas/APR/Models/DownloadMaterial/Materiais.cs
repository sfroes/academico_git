using System.Collections.Generic;

namespace SMC.Academico.UI.Mvc.Areas.APR.Models.DownloadMaterial
{
    public class Node
    {
        public string id { get; set; }
    }

    public class NodesOrder
    {
        public string id { get; set; }
        public List<Node> nodes { get; set; }
    }

    public class CheckedNode
    {
        public string value { get; set; }
        public bool @checked { get; set; }
    }

    public class RootObject
    {
        public string name { get; set; }
        public List<NodesOrder> nodesOrder { get; set; }
        public List<CheckedNode> checkedNodes { get; set; }
    }
}
