using System.Collections;
using System.Collections.Generic;
using Villekoskela.Utils;
using UnityEngine;

namespace Dango.Other
{
    public class DynamicAtlas
    {
        private List<Rect> rectangles = new List<Rect>();
        private IntegerRectangle integerRect = new IntegerRectangle();
        private RectanglePacker packer = new RectanglePacker();
        private Dictionary<string, Sprite> dictionary = new Dictionary<string, Sprite>();

        private void TextureClear(Texture2D texture)
        {
            Color32[] fillColor = texture.GetPixels32();
            for (int i = 0; i < fillColor.Length; ++i)
                fillColor[i] = Color.clear;
        }

        public bool CreateAtlas(Sprite[] sprites, int textureSize = 1024, int padding = 1)
        {
            bool result = false;
            Texture2D texture = new Texture2D(textureSize, textureSize, TextureFormat.ARGB32, false);

            rectangles.Clear();
            dictionary.Clear();
            packer.ResetAll(texture.width, texture.height, padding);

            for (int i = 0; i < sprites.Length; i++)
            {
                rectangles.Add(new Rect(0, 0, sprites[i].texture.width, sprites[i].texture.height));
            }

            for (int i = 0; i < rectangles.Count; i++)
            {
                packer.insertRectangle((int) rectangles[i].width, (int) rectangles[i].height, i);
            }

            packer.packRectangles();

            int index = 0;
            Color32[] colors = null;
            Sprite sprite = null;
            if (packer.rectangleCount > 0)
            {
                for (int j = 0; j < packer.rectangleCount; j++)
                {
                    integerRect = packer.getRectangle(j, integerRect);
                    index = packer.getRectangleId(j);

                    //TODO 此处会有大量GC，后续考虑优化处理或在场景切换时使用
                    colors = sprites[index].texture.GetPixels32();
                    texture.SetPixels32(integerRect.x, integerRect.y, integerRect.width, integerRect.height, colors);
                    sprite = Sprite.Create(texture,
                        new Rect(integerRect.x, integerRect.y, integerRect.width, integerRect.height), Vector2.zero,
                        100f, 0, SpriteMeshType.FullRect);

                    if (!dictionary.ContainsKey(sprites[index].name))
                    {
                        dictionary.Add(sprites[index].name, sprite);
                    }
                }

                texture.name = "texture Test";
                texture.Apply();
                result = true;
            }

            return result;
        }

        public int SpriteCount
        {
            get { return dictionary.Count; }
        }

        public bool TryGetSprite(string spriteName, out Sprite sprite)
        {
            return dictionary.TryGetValue(spriteName, out sprite);
        }
    }
}