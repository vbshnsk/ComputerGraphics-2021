using System;
using System.Linq;
using ComputerGraphics.renderer.@struct;

namespace ComputerGraphics.renderer.reader
{
    public class ObjParserObjReader : IObjReader
    {
        public Triangle[] Read(string path)
        {
            var obj = new ObjParser.Obj();
            obj.LoadObj(path);
            return obj.FaceList.Select(
                v =>
                {
                    var p1 = new Vector3(
                        (float) obj.VertexList[v.VertexIndexList[0] - 1].X,
                        (float) obj.VertexList[v.VertexIndexList[0] - 1].Y,
                        (float) obj.VertexList[v.VertexIndexList[0] - 1].Z);
                    var p2 = new Vector3(
                        (float) obj.VertexList[v.VertexIndexList[1] - 1].X,
                        (float) obj.VertexList[v.VertexIndexList[1] - 1].Y,
                        (float) obj.VertexList[v.VertexIndexList[1] - 1].Z);
                    var p3 = new Vector3(
                        (float) obj.VertexList[v.VertexIndexList[2] - 1].X,
                        (float) obj.VertexList[v.VertexIndexList[2] - 1].Y,
                        (float) obj.VertexList[v.VertexIndexList[2] - 1].Z);
                    return new Triangle(
                        p1,
                        p2,
                        p3
                    );
                }
            ).ToArray();
        }
    }
}