using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TilesController : MonoBehaviour
{
    public CameraController cameraController;
    public int lenght;
    public int height;
    private int gridDimension;
    public int Maxlevel;

    public int radius;
    
    [Range (0f, 100f)]    

    public int desiredlevel;
    public Gradient gradient;
    public GameObject Prefab;

    bool generated= false;

    GameObject[,] tilegrid;

    public float waittime=0.2f;

    int selObjXpos;
    int selObjYpos;
    bool ClickDone = false;

    void Start()
    {

        

    }

    void Update()
    {
            //check onmouseclick gameobject

            if (Input.GetMouseButtonDown(0))
        { 
            Vector3 raycastPos = Camera.main.ScreenToWorldPoint( new Vector3Int((int)Input.mousePosition.x,(int)Input.mousePosition.y, (int)Camera.main.transform.position.z*-1));
            Vector2 raycastPos2D = new Vector2(raycastPos.x, raycastPos.y);
            Debug.Log("mouse pressed on, 2D:" +raycastPos2D +"3D:"+raycastPos);
            RaycastHit2D hit =Physics2D.Raycast(raycastPos2D, Vector2.zero);
            if(!ClickDone)
            {

              if (hit.collider !=null)
                 {

                     GameObject selectedObj;
                     selectedObj = hit.transform.gameObject;

                    Debug.Log("collision with gameobject detected");
                     CurrentClickedGameObject(selectedObj);

                    selObjXpos = (int)selectedObj.transform.position.x;
                    selObjYpos = (int)selectedObj.transform.position.y;
                 }
             
            }
        }


        if(Input.GetKeyDown(KeyCode.Space))
        {
                
                CreateNewGrid();
                
        }
    }
    

    public void CreateNewGrid()
    {   if(generated)
        {
            foreach(GameObject tileobj in tilegrid)
            {
                Destroy(tileobj);
            }
            generated =false;
        }

        tilegrid = new GameObject[lenght,height];
        gridDimension = lenght * height;
        generated =true;        
        int i =0;

     do{
        for(int x =0; x < lenght ;x++)
        {
            for(int y =0; y < height ;y++)
            {
                Vector3 pos= new Vector3Int(x,y,0);
                GameObject newtileobj;
                newtileobj = Instantiate(Prefab, pos, Quaternion.identity);
                tilegrid[x,y] =newtileobj;
                int level = Random.Range (1, Maxlevel);
                newtileobj.name = "Tile " + i + "," + x +","+y+ ","+ "L"+ level;
                newtileobj.GetComponent<SpriteRenderer>().color = gradient.Evaluate((float)level/(float)Maxlevel);

                i++;
                
            }
        }        
}
    while(i <gridDimension);
    }

IEnumerator FloodFill(int _xpos, int _ypos)
{
    Color newColor = gradient.Evaluate((float)desiredlevel/(float)Maxlevel);
    if(_xpos>=0 && _xpos<lenght &&_ypos>=0 && _ypos<height)

        {
            WaitForSeconds wait =new WaitForSeconds (waittime);
            yield return wait;
            if(_xpos >= (selObjXpos-radius) &&_xpos<= (selObjXpos+radius) && _ypos >= (selObjYpos-radius) && _ypos <= (selObjYpos+radius))
            {
                
                if(tilegrid[_xpos,_ypos].GetComponent<SpriteRenderer>().color !=newColor)
                {
                    ChangeLevel(_xpos,_ypos);
                    StartCoroutine(FloodFill(_xpos +1,   _ypos   ));
                    StartCoroutine(FloodFill(_xpos +1,   _ypos +1));
                    StartCoroutine(FloodFill(_xpos +1,   _ypos -1));
                    StartCoroutine(FloodFill(_xpos   ,   _ypos -1));
                    StartCoroutine(FloodFill(_xpos -1,   _ypos   ));
                    StartCoroutine(FloodFill(_xpos -1,   _ypos -1));
                    StartCoroutine(FloodFill(_xpos -1,   _ypos +1));
                    StartCoroutine(FloodFill(_xpos   ,   _ypos +1));
                }
            }  
        }
}

    public void ChangeLevel(int _x, int _y)
    {
        if(tilegrid[_x,_y]!=null)
        {
            tilegrid[_x, _y].GetComponent<SpriteRenderer>().color = gradient.Evaluate((float)desiredlevel/(float)Maxlevel);
        }
    } 

     public void CurrentClickedGameObject(GameObject _gobj)
 {
        int xpos = (int)_gobj.transform.position.x;
        Debug.Log(xpos);

        int ypos = (int)_gobj.transform.position.y;
        Debug.Log(ypos);

     StartCoroutine(FloodFill(xpos,ypos));
 }

}

  





