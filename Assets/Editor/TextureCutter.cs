using UnityEngine;
using UnityEditor;
using System.IO;

public class TextureCutter : EditorWindow
{
    private Texture2D sourceTexture;
    private int sliceWidth = 64;
    private int sliceHeight = 64;

    [MenuItem("Window/Texture Cutter")]
    public static void ShowWindow()
    {
        GetWindow<TextureCutter>("Texture Cutter");
    }

    void OnGUI()
    {
        GUILayout.Label("Chia Texture thành nhiều ảnh nhỏ", EditorStyles.boldLabel);
        sourceTexture = (Texture2D)EditorGUILayout.ObjectField("Texture Gốc", sourceTexture, typeof(Texture2D), false);
        sliceWidth = EditorGUILayout.IntField("Chiều rộng mỗi ảnh con", sliceWidth);
        sliceHeight = EditorGUILayout.IntField("Chiều cao mỗi ảnh con", sliceHeight);

        if (GUILayout.Button("Cắt Ảnh"))
        {
            if (sourceTexture != null)
                SliceTexture(sourceTexture, sliceWidth, sliceHeight);
            else
                Debug.LogError("❌ Hãy chọn một Texture trước!");
        }
    }

    void SliceTexture(Texture2D texture, int width, int height)
    {
        string path = AssetDatabase.GetAssetPath(texture);
        string directory = Path.GetDirectoryName(path);
        string filename = Path.GetFileNameWithoutExtension(path);

        for (int y = 0; y < texture.height; y += height)
        {
            for (int x = 0; x < texture.width; x += width)
            {
                Texture2D croppedTexture = new Texture2D(width, height);
                croppedTexture.SetPixels(texture.GetPixels(x, y, width, height));
                croppedTexture.Apply();

                // Lưu Texture mới vào thư mục Assets
                byte[] bytes = croppedTexture.EncodeToPNG();
                string savePath = $"{directory}/{filename}_part_{x}_{y}.png";
                File.WriteAllBytes(savePath, bytes);
            }
        }

        AssetDatabase.Refresh();
        Debug.Log("✅ Ảnh đã được cắt và lưu vào thư mục Assets!");
    }
}