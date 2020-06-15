using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Villekoskela.Utils;

namespace Dango.Other
{
    public class DynamicAtlas
    {
        private RenderTexture renderTexture;
        private Material renderMateral;

        private List<Rect> rectangles = new List<Rect>();
        private IntegerRectangle integerRect = new IntegerRectangle();
        private RectanglePacker packer = new RectanglePacker();
        private Dictionary<string, Rect> dictionary = new Dictionary<string, Rect>();

        public bool CreateAtlas(Texture[] textures, int width = 1024, int height = 1024, int padding = 1)
        {
            bool result = false;

            rectangles.Clear();
            dictionary.Clear();
            packer.ResetAll(width, height, padding);

            CreateMaterial();
            CreateRenderTexture(width, height);

            for (int i = 0; i < textures.Length; i++)
            {
                rectangles.Add(new Rect(0, 0, textures[i].width, textures[i].height));
            }

            for (int i = 0; i < rectangles.Count; i++)
            {
                packer.insertRectangle((int) rectangles[i].width, (int) rectangles[i].height, i);
            }

            packer.packRectangles();

            int index = 0;
            if (packer.rectangleCount > 0)
            {
                for (int j = 0; j < packer.rectangleCount; j++)
                {
                    integerRect = packer.getRectangle(j, integerRect);
                    index = packer.getRectangleId(j);

                    if (!dictionary.ContainsKey(textures[index].name))
                    {
                        Rect rect = new Rect();
                        rect.x = (float) integerRect.x / width;
                        rect.y = (float) integerRect.y / height;
                        rect.width = (float) integerRect.width / width;
                        rect.height = (float) integerRect.height / height;

                        dictionary.Add(textures[index].name, rect);
                    }

                    DrawTexture(textures[index], renderTexture,
                        new Rect(integerRect.x, integerRect.y, integerRect.width, integerRect.height));
                }

                result = true;
            }

            return result;
        }

        public int SpriteCount
        {
            get { return dictionary.Count; }
        }

        public void SetRawImage(string name, RawImage rawImage)
        {
            Rect uv = new Rect();
            if (rawImage != null && TryGetSprite(name, out uv))
            {
                rawImage.texture = renderTexture;
                rawImage.uvRect = uv;
            }
        }

        public bool TryGetSprite(string spriteName, out Rect rect)
        {
            return dictionary.TryGetValue(spriteName, out rect);
        }

        public void Release()
        {
            if (renderTexture != null)
            {
                RenderTexture.ReleaseTemporary(renderTexture);
                renderTexture = null;
            }
        }

        private void CreateMaterial()
        {
            if (renderMateral == null)
            {
                renderMateral = new Material(Shader.Find("UI/DynamicAtlas"));
            }
        }

        private void CreateRenderTexture(int width, int height)
        {
            Release();
            renderTexture = RenderTexture.GetTemporary(width, height, 0, RenderTextureFormat.BGRA32);
        }

        private void DrawTexture(Texture source, RenderTexture target, Rect rect)
        {
            if (source == null || target == null)
                return;

            float l = rect.x * 2.0f / target.width - 1;
            float r = (rect.x + rect.width) * 2.0f / target.width - 1;
            float b = rect.y * 2.0f / target.height - 1;
            float t = (rect.y + rect.height) * 2.0f / target.height - 1;
            var mat = new Matrix4x4();
            mat.m00 = r - l;
            mat.m03 = l;
            mat.m11 = t - b;
            mat.m13 = b;
            mat.m23 = -1;
            mat.m33 = 1;

            renderMateral.SetMatrix(Shader.PropertyToID("_ImageMVP"), GL.GetGPUProjectionMatrix(mat, true));
            Graphics.Blit(source, target, renderMateral);
        }
    }
}