﻿using UnityEngine;

namespace uDesktopDuplication
{

public class Monitor
{
    public Monitor(int id)
    {
        this.id = id;
    }

    public int id 
    { 
        get; 
        private set; 
    }

    public bool exists
    { 
        get { return id < Manager.monitorCount; } 
    }

    public string name
    { 
        get { return Lib.GetName(id); }
    }

    public bool isPrimary
    { 
        get { return Lib.IsPrimary(id); }
    }

    public int width 
    { 
        get { return Lib.GetWidth(id); }
    }

    public int height
    { 
        get { return Lib.GetHeight(id); }
    }

    public float aspect
    { 
        get { return (float)width / height; }
    }

    public bool isHorizontal
    { 
        get { return width > height; }
    }

    public bool isVertical 
    { 
        get { return height > width; }
    }

    public bool isCursorVisible
    { 
        get { return Lib.IsCursorVisible(id); }
    }

    public int pointerX
    { 
        get { return Lib.GetCursorX(id); }
    }

    public int pointerY
    { 
        get { return Lib.GetCursorY(id); }
    }

    public int pointerShapeWidth
    { 
        get { return Lib.GetCursorShapeWidth(id); }
    }

    public int pointerShapeHeight
    { 
        get { return Lib.GetCursorShapeHeight(id); }
    }

    public CursorShapeType pointerShapeType
    { 
        get { return Lib.GetCursorShapeType(id); }
    }

    public bool shouldBeUpdated
    {
        get; 
        set;
    }

    private Texture2D texture_;
    public Texture2D texture 
    {
        get 
        { 
            if (texture_ == null) {
                CreateTexture();
            }
            return texture_;
        }
    }

    public void Render()
    {
        Lib.SetTexturePtr(id, texture_.GetNativeTexturePtr());
        GL.IssuePluginEvent(Lib.GetRenderEventFunc(), id);
    }

    public void UpdateCursorTexture(System.IntPtr ptr)
    {
        Lib.UpdateCursorTexture(id, ptr);
    }

    void CreateTexture()
    {
        var w = isHorizontal ? width : height;
        var h = isHorizontal ? height : width;
        bool shouldCreate = true;

        if (texture_) {
            if (texture_.width != w || texture_.height != h) {
                if (texture_) Object.DestroyImmediate(texture_);
                shouldCreate = true;
            } else { 
                shouldCreate = false;
            }
        }

        if (shouldCreate) {
            texture_ = new Texture2D(w, h, TextureFormat.BGRA32, false);
        }
    }

    public void Reinitialize()
    {
        CreateTexture();
    }
}

}