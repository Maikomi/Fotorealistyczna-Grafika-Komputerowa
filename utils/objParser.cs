using System.Globalization;


namespace Vector
{
    // Klasa do przechowywania twarzy (face)
    public class Face
    {
        public List<int> VertexIndices { get; private set; } = new List<int>();
        public List<int> TextureIndices { get; private set; } = new List<int>();
        public List<int> NormalIndices { get; private set; } = new List<int>();

        public override string ToString()
        {
            return $"Face({string.Join(", ", VertexIndices)})";
        }
    }

    // Klasa parsera OBJ
    public class ObjParser
    {
        public List<Vector> Vertices { get; private set; } = new List<Vector>();
        public List<Vector> Normals { get; private set; } = new List<Vector>();
        public List<Face> Faces { get; private set; } = new List<Face>();

        public void Load(string path)
        {
            using (StreamReader sr = new StreamReader(path))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    ParseLine(line);
                }
            }
        }

        private void ParseLine(string line)
        {
            string[] tokens = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (tokens.Length == 0) return;

            switch (tokens[0])
            {
                case "v":  // Wierzchołki
                    Vertices.Add(ParseVector(tokens));
                    break;
                case "vn":  // Normalne
                    Normals.Add(ParseVector(tokens));
                    break;
                case "f":  // Twarze (face)
                    Faces.Add(ParseFace(tokens));
                    break;
            }
        }

        private Vector ParseVector(string[] tokens)
        {
            float x = float.Parse(tokens[1], CultureInfo.InvariantCulture);
            float y = float.Parse(tokens[2], CultureInfo.InvariantCulture);
            float z = float.Parse(tokens[3], CultureInfo.InvariantCulture);
            return new Vector(x, y, z);
        }

        private Face ParseFace(string[] tokens)
        {
            var face = new Face();
            for (int i = 1; i < tokens.Length; i++)
            {
                string[] indices = tokens[i].Split('/');
                face.VertexIndices.Add(int.Parse(indices[0]) - 1);

                if (indices.Length > 1 && indices[1] != "")
                    face.TextureIndices.Add(int.Parse(indices[1]) - 1);
                if (indices.Length > 2)
                    face.NormalIndices.Add(int.Parse(indices[2]) - 1);
            }
            return face;
        }

        // Przykładowa metoda do wyświetlenia załadowanych danych
        public void PrintData()
        {
            Console.WriteLine("Vertices:");
            foreach (var vertex in Vertices)
            {
                Console.WriteLine(vertex);
            }

            Console.WriteLine("\nNormals:");
            foreach (var normal in Normals)
            {
                Console.WriteLine(normal);
            }

            Console.WriteLine("\nFaces:");
            foreach (var face in Faces)
            {
                Console.WriteLine(face);
            }
        }
    }
}