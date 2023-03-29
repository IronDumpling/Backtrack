//using UnityEngine;

//public class TurningTrackCollider : MonoBehaviour
//{
//    [SerializeField] private int numCubes = 10;
//    [SerializeField] private float cubeSize = 1f;
//    [SerializeField] private float trackRadius = 5f;

//    private void Start()
//    {
//        GenerateBound(30);
//    }

//    private void GenerateBound(int degree)
//    {
//        // Create a new empty GameObject to hold the colliders
//        var colliderObject = new GameObject("TurningTrackColliders");

//        // Calculate the angle between each cube
//        var angle = Mathf.Deg2Rad * degree / numCubes;

//        // Create a series of cube meshes that approximate the shape of the turning track
//        var meshes = new Mesh[numCubes];
//        for (int i = 0; i < numCubes; i++)
//        {
//            // Create a new cube GameObject as a child of the empty GameObject
//            var cubeObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
//            cubeObject.transform.parent = colliderObject.transform;

//            // Adjust the position, rotation, and scale of the cube to fit the curve of the turning track
//            var x = Mathf.Sin(angle * i) * trackRadius;
//            var z = Mathf.Cos(angle * i) * trackRadius;
//            var y = 0f;
//            var position = new Vector3(x, y, z);
//            cubeObject.transform.localPosition = position;

//            var rotation = Quaternion.Euler(0f, -angle * i * Mathf.Rad2Deg, 0f);
//            cubeObject.transform.localRotation = rotation;

//            var scale = new Vector3(cubeSize, cubeSize, trackRadius * angle);
//            cubeObject.transform.localScale = scale;

//            // Add a MeshCollider component to the cube
//            var meshFilter = cubeObject.GetComponent<MeshFilter>();
//            meshes[i] = meshFilter.mesh;
//            var meshCollider = cubeObject.AddComponent<MeshCollider>();
//            meshCollider.sharedMesh = meshFilter.mesh;
//        }

//        // Combine the meshes of all the cubes into a single mesh using the CombineMeshes method
//        var combinedMesh = new Mesh();
//        combinedMesh.CombineMeshes(meshes);

//        // Assign the combined mesh to the MeshCollider component of the empty GameObject
//        var collider = colliderObject.AddComponent<MeshCollider>();
//        collider.sharedMesh = combinedMesh;
//    }
//}
using UnityEngine;

public class TurningTrackCollider : MonoBehaviour
{
    public GameObject cubePrefab;
    public int cubeCountPerCurve = 5;
    public float cubeSize = 0.1f;

    void Start()
    {
        // Get the track object and its mesh
        GameObject track = GameObject.Find("Turn_track_30_0");
        MeshFilter meshFilter = track.GetComponent<MeshFilter>();

        if (meshFilter != null)
        {
            // Get the mesh and its vertices
            Mesh mesh = meshFilter.mesh;
            Vector3[] vertices = mesh.vertices;

            // Calculate the number of cubes needed per curve
            int numCubesPerCurve = Mathf.CeilToInt((float)vertices.Length / cubeCountPerCurve);

            // Loop through each curve
            for (int i = 0; i < numCubesPerCurve; i++)
            {
                int startIndex = i * cubeCountPerCurve;
                int endIndex = Mathf.Min(startIndex + cubeCountPerCurve, vertices.Length);
                int numCubes = endIndex - startIndex;

                // Create the cube array
                GameObject[] cubes = new GameObject[numCubes];

                // Loop through each vertex in the curve and create a cube
                for (int j = startIndex; j < endIndex; j++)
                {
                    // Create the cube and position it on the vertex
                    Vector3 position = vertices[j];
                    GameObject cube = Instantiate(cubePrefab, position, Quaternion.identity, transform);

                    // Scale the cube to the desired size
                    cube.transform.localScale = new Vector3(cubeSize, cubeSize, cubeSize);

                    // Add the cube to the array
                    cubes[j - startIndex] = cube;
                }

                // Combine the meshes of all the cubes in the array
                MeshFilter[] meshFilters = new MeshFilter[numCubes];
                CombineInstance[] combine = new CombineInstance[numCubes];
                for (int j = 0; j < numCubes; j++)
                {
                    meshFilters[j] = cubes[j].GetComponent<MeshFilter>();
                    combine[j].mesh = meshFilters[j].sharedMesh;
                    combine[j].transform = meshFilters[j].transform.localToWorldMatrix;
                    Destroy(cubes[j]);
                }

                Mesh meshCombined = new Mesh();
                meshCombined.CombineMeshes(combine);
                GameObject combinedObject = new GameObject("CurveCollider");
                MeshFilter meshFilterCombined = combinedObject.AddComponent<MeshFilter>();
                MeshRenderer meshRendererCombined = combinedObject.AddComponent<MeshRenderer>();
                meshFilterCombined.mesh = meshCombined;
                meshRendererCombined.material = cubePrefab.GetComponent<MeshRenderer>().sharedMaterial;
                combinedObject.transform.SetParent(transform);
            }
        }
    }
}


