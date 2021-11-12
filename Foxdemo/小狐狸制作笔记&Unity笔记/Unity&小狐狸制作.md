# Unity&小狐狸制作

- 主体页面

  - Project - 文件目录

    位于default界面的的正下方，可以存放各种素材

  - Scene  -游戏画面

    每一个场景

  - Game-游戏

    实操游戏

- 素材获取
  - asset store - 素材商店
    通过asset store，可以下载所需要的素材

- 单元

  单元(Unit)是指每个方格里面有多少个像素点，此次制作我们选择**16**的单元，单元是对于素材所修改的

- 网格

  在创建(+)中找到2D对象中的瓦片地图选择矩形则可以**出现以设置像素为单位的网格**，效果图如下

<img src="Unity&小狐狸制作.assets/Tilemap.png" alt="Tilemap" style="zoom: 67%;" />

- 取消显示

  选中scene中任意一个对象，选中如下图，则会隐藏目标<img src="Unity&小狐狸制作.assets/Inspector.png" alt="Inspector" style="zoom:67%;" />

- 切割

  - 平铺调色板

    在***窗口***中移动到2D即可找到***调色面板***

  - 待切割图像

    将待切割图像的像素设置成需要的像素大小

    选中待切割图像在sprite mode调成multiple，并设置需要切割的像素大小，我们可以获得想要切割的部分，如图

    ![Sprite](Unity&小狐狸制作.assets/Sprite.png)

    我们如果想要细分每个单元的话我们需要在转跳出来的页面选择切割，并且如图，我们可以获得每个切割图形的的单位像素

    ![Slice](Unity&小狐狸制作.assets/Slice.png)

    在调用出的***调色面板***中将所需要切割的图像拖拽到***调色面板***中

    点击保存

- 调整游戏画面

  - 可视比例

    在***游戏***中可调整画面比例，如调成16:9

  - 摄像头观测范围

    在***Main Camera***中调整参数中的***大小***

- 图层排序

  - sorting layer

    越在下面，越在前面

  - order layer

    数字越大越在前，同层级的时候使用

- 创建对象
  - 在下方文件处找到对象模型，拖拽到scence中即可创建
  - 在左方右键创建sprite。将对象模型拖拽到 sprite(精灵)

- 添加组件—Add Component

  - 2d刚体—rigibody 2d

    添加后让目标成为实体，赋有重力等物理量

  - 2d碰撞体—box collider 2d

    添加后对目标在一定范围内拥有碰撞效果

  - 2d地图碰撞格子—tilemap collider 2d

    添加后对地图中所有填充过的区域进行碰撞体化

- 整体控制更改
  - Edit(编辑)中Project Setting - input(输入管理)可以更改

- 自主添加脚本

  - add Component
    new script可以命名，会自动创建一个C#的脚本，这个脚本将创建在Assets中

  - 管理

    为了更好的管理脚本，我们可以创建一个文件夹，存放脚本

- 移动

  - public Rigidbody2D rb; 定义刚体。可以手动在图形编辑器里拖进去添加到对象的组件中

  - Input.GetAxis(Horizontal); 获取横轴变更

  -  rb.velocity = new Vector2(horizontalMove * speed, rb.velocity.y); 设置对象位置

  - 详细设置讲解

    ```c#
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    
    public class PlayerController : MonoBehaviour
    {
        public Rigidbody2D rb;
        public float speed;
    
    
    
    
        // Start is called before the first frame update
        void Start()
        {
            
        }
    
        // Update is called once per frame
        void Update()
        {
            Movement();
        }
        void Movement()
        {
            float Horizontalmove;
            Horizontalmove = Input.GetAxis("Horizontal");
            if (Horizontalmove != 0) {
                rb.velocity = new Vector2(Horizontalmove * speed, rb.velocity.y);
            }
        }
    
        
    }
    ```

    
