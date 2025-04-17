namespace RpgGame
{
    public class ObjData
    {
        public string sUid;

        public string name;

        public TransformData transform;

        public ObjData(TransformData transform) 
        {
            sUid = System.Guid.NewGuid().ToString();
            this.transform = transform;
        }

        public ObjData(string name, TransformData transform) 
        {
            sUid = System.Guid.NewGuid().ToString();
            this.transform = transform;
            this.name = name;
        }
    }
}