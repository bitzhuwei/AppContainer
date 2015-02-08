# AppContainer
Embed an exe application into the panel control. make it feels like you own this exe application.
http://www.cnblogs.com/bitzhuwei/archive/2012/05/24/SmileWei_EmbeddedApp.html
<p>这是最近在做的一个项目中提到的需求，把一个现有的窗体应用程序界面嵌入到自己开发的窗体中来，看起来就像自己开发的一样（实际上&hellip;&hellip;跟自己开发的还是有一点点区别的，就是内嵌程序和宿主程序的窗口激活状态问题）。</p>
<p>在codeproject找到了一篇相关的文章（<a title="Hosting EXE Applications in a WinForm project" href="http://www.codeproject.com/Articles/9123/Hosting-EXE-Applications-in-a-WinForm-project" target="_blank">http://www.codeproject.com/Articles/9123/Hosting-EXE-Applications-in-a-WinForm-project</a>），虽然可用，但是很不方便，于是重新设计编写了一个类库，用一个控件完成内嵌其它应用程序的功能。</p>
<p>直接上图先：</p>
<p>&nbsp;<a href="http://images.cnblogs.com/cnblogs_com/bitzhuwei/201205/201205241831233469.png"><img style="display: inline; border-width: 0px;" title="嵌入QQ影音" src="http://images.cnblogs.com/cnblogs_com/bitzhuwei/201205/201205241831247863.png" alt="嵌入QQ影音" width="365" height="254" border="0" /></a> <a href="http://images.cnblogs.com/cnblogs_com/bitzhuwei/201205/201205241831258877.png"><img style="display: inline; border-width: 0px;" title="嵌入Windows Live Writer" src="http://images.cnblogs.com/cnblogs_com/bitzhuwei/201205/201205241831258287.png" alt="嵌入Windows Live Writer" width="327" height="254" border="0" /></a></p>
<p><a href="http://images.cnblogs.com/cnblogs_com/bitzhuwei/201205/20120524183127630.png"><img style="display: inline; border-width: 0px;" title="嵌入photoshop" src="http://images.cnblogs.com/cnblogs_com/bitzhuwei/201205/201205241831279169.png" alt="嵌入photoshop" width="389" height="254" border="0" /></a> <a href="http://images.cnblogs.com/cnblogs_com/bitzhuwei/201205/201205241831289103.png"><img style="display: inline; border-width: 0px;" title="嵌入Adobe Reader" src="http://images.cnblogs.com/cnblogs_com/bitzhuwei/201205/201205241831288513.png" alt="嵌入Adobe Reader" width="355" height="254" border="0" /></a></p>
<p>从打开Adobe Reader那张图片可以看出来所谓的&ldquo;内嵌程序和宿主程序的窗口激活状态问题&rdquo;。当内嵌程序窗口激活时，表面上将其包裹起来的宿主窗口却处于非激活的状态。想隐藏这一点的话，把窗口的FormBorderStyle属性设为None吧，然后自己在窗口上画关闭、最大化、最小化按钮好了。</p>
<p>&nbsp;</p>
<p>原作者的实现思路更能暴露本质，所以这里用原作者的代码段解释一下实现过程。</p>
<p>1、启动要嵌入的应用程序进程</p>
<div class="cnblogs_code">
<pre><span style="color: #008080;"> 1</span> Process p = <span style="color: #0000ff;">null</span><span style="color: #000000;">; 
</span><span style="color: #008080;"> 2</span> <span style="color: #0000ff;">try</span> 
<span style="color: #008080;"> 3</span> <span style="color: #000000;">{
</span><span style="color: #008080;"> 4</span>   <span style="color: #008000;">//</span><span style="color: #008000;"> Start the process </span>
<span style="color: #008080;"> 5</span>   p = System.Diagnostics.Process.Start(<span style="color: #0000ff;">this</span><span style="color: #000000;">.exeName); 
</span><span style="color: #008080;"> 6</span> 
<span style="color: #008080;"> 7</span>   <span style="color: #008000;">//</span><span style="color: #008000;"> Wait for process to be created and enter idle condition </span>
<span style="color: #008080;"> 8</span> <span style="color: #000000;">  p.WaitForInputIdle(); 
</span><span style="color: #008080;"> 9</span> 
<span style="color: #008080;">10</span>   <span style="color: #008000;">//</span><span style="color: #008000;"> Get the main handle</span>
<span style="color: #008080;">11</span>   appWin =<span style="color: #000000;"> p.MainWindowHandle; 
</span><span style="color: #008080;">12</span> <span style="color: #000000;">} 
</span><span style="color: #008080;">13</span> <span style="color: #0000ff;">catch</span><span style="color: #000000;"> (Exception ex) 
</span><span style="color: #008080;">14</span> <span style="color: #000000;">{ 
</span><span style="color: #008080;">15</span>   MessageBox.Show(<span style="color: #0000ff;">this</span>, ex.Message, <span style="color: #800000;">"</span><span style="color: #800000;">Error</span><span style="color: #800000;">"</span><span style="color: #000000;">); 
</span><span style="color: #008080;">16</span> }</pre>
</div>
<p>2、调用Windows API将启动的应用程序窗口嵌入自定义的控件（作者用的是Panel控件）</p>
<div class="cnblogs_code">
<pre><span style="color: #008080;">1</span> <span style="color: #008000;">//</span><span style="color: #008000;"> Put it into this form</span>
<span style="color: #008080;">2</span> SetParent(appWin, <span style="color: #0000ff;">this</span><span style="color: #000000;">.Handle);//<span style="color: #008000;">this在这里是Panel控件<br /><br /></span>
</span><span style="color: #008080;">3</span> 
<span style="color: #008080;">4</span> <span style="color: #008000;">//</span><span style="color: #008000;"> Remove border and whatnot</span>
<span style="color: #008080;">5</span> <span style="color: #000000;">SetWindowLong(appWin, GWL_STYLE, WS_VISIBLE);
</span><span style="color: #008080;">6</span> 
<span style="color: #008080;">7</span> <span style="color: #008000;">//</span><span style="color: #008000;"> Move the window to overlay it on this window</span>
<span style="color: #008080;">8</span> MoveWindow(appWin, <span style="color: #800080;">0</span>, <span style="color: #800080;">0</span>, <span style="color: #0000ff;">this</span>.Width, <span style="color: #0000ff;">this</span>.Height, <span style="color: #0000ff;">true</span>);</pre>
</div>
<p>3、设置被嵌入的窗体大小随宿主窗体改变</p>
<div class="cnblogs_code">
<pre><span style="color: #008080;">1</span> <span style="color: #0000ff;">protected</span> <span style="color: #0000ff;">override</span> <span style="color: #0000ff;">void</span><span style="color: #000000;"> OnResize(EventArgs e)
</span><span style="color: #008080;">2</span> <span style="color: #000000;">{
</span><span style="color: #008080;">3</span>   <span style="color: #0000ff;">if</span> (<span style="color: #0000ff;">this</span>.appWin !=<span style="color: #000000;"> IntPtr.Zero)
</span><span style="color: #008080;">4</span> <span style="color: #000000;">  {
</span><span style="color: #008080;">5</span>     MoveWindow(appWin, <span style="color: #800080;">0</span>, <span style="color: #800080;">0</span>, <span style="color: #0000ff;">this</span>.Width, <span style="color: #0000ff;">this</span>.Height, <span style="color: #0000ff;">true</span><span style="color: #000000;">);
</span><span style="color: #008080;">6</span> <span style="color: #000000;">  }
</span><span style="color: #008080;">7</span>   <span style="color: #0000ff;">base</span><span style="color: #000000;">.OnResize (e);
</span><span style="color: #008080;">8</span> }</pre>
</div>
<p>4、设置被嵌入的窗体应用程序在宿主程序关闭时也关闭</p>
<div class="cnblogs_code">
<pre><span style="color: #008080;"> 1</span> <span style="color: #0000ff;">protected</span> <span style="color: #0000ff;">override</span> <span style="color: #0000ff;">void</span><span style="color: #000000;"> OnHandleDestroyed(EventArgs e)
</span><span style="color: #008080;"> 2</span> <span style="color: #000000;">{
</span><span style="color: #008080;"> 3</span>   <span style="color: #008000;">//</span><span style="color: #008000;"> Stop the application</span>
<span style="color: #008080;"> 4</span>   <span style="color: #0000ff;">if</span> (appWin !=<span style="color: #000000;"> IntPtr.Zero)
</span><span style="color: #008080;"> 5</span> <span style="color: #000000;">  {
</span><span style="color: #008080;"> 6</span>     <span style="color: #008000;">//</span><span style="color: #008000;"> Post a colse message</span>
<span style="color: #008080;"> 7</span>     PostMessage(appWin, WM_CLOSE, <span style="color: #800080;">0</span>, <span style="color: #800080;">0</span><span style="color: #000000;">);
</span><span style="color: #008080;"> 8</span> 
<span style="color: #008080;"> 9</span>     <span style="color: #008000;">//</span><span style="color: #008000;"> Delay for it to get the message</span>
<span style="color: #008080;">10</span>     System.Threading.Thread.Sleep(<span style="color: #800080;">1000</span><span style="color: #000000;">);
</span><span style="color: #008080;">11</span> 
<span style="color: #008080;">12</span>     <span style="color: #008000;">//</span><span style="color: #008000;"> Clear internal handle</span>
<span style="color: #008080;">13</span>     appWin =<span style="color: #000000;"> IntPtr.Zero;
</span><span style="color: #008080;">14</span> <span style="color: #000000;">  }
</span><span style="color: #008080;">15</span>   <span style="color: #0000ff;">base</span><span style="color: #000000;">.OnHandleDestroyed (e);
</span><span style="color: #008080;">16</span> }</pre>
</div>
<p>&nbsp;</p>
<p>原作者的代码实际用起来是很不方便的，具体大家试试就知道，不细说了（反正我只学了学上面的步骤，也不用他的库）。</p>
<p>本人开发了一个比较实用的控件，使用起来也很简单，只需三步。</p>
<p>首先，在窗体应用程序项目中引用类库SmileWei.EmbeddedApp。</p>
<p><a href="http://images.cnblogs.com/cnblogs_com/bitzhuwei/201205/201205241831286528.png"><img style="display: inline; border-width: 0px;" title="在窗体应用程序项目中引用类库SmileWei.EmbeddedApp" src="http://images.cnblogs.com/cnblogs_com/bitzhuwei/201205/201205241831298447.png" alt="在窗体应用程序项目中引用类库SmileWei.EmbeddedApp" width="218" height="244" border="0" /></a></p>
<p>然后，在宿主窗体上拖一个AppContainer控件，摆放好位置。（如果工具箱里没有AppContainer，就F6生成解决方案一下，然后再看就有了。）</p>
<p><a href="http://images.cnblogs.com/cnblogs_com/bitzhuwei/201205/201205242025245790.png"><img style="display: inline; border-width: 0px;" title="在宿主窗体上拖一个AppContainer控件，摆放好位置" src="http://images.cnblogs.com/cnblogs_com/bitzhuwei/201205/20120524202527717.png" alt="在宿主窗体上拖一个AppContainer控件，摆放好位置" width="696" height="424" border="0" /></a></p>
<p>最后，告诉AppContainer控件，要嵌入的应用程序(*.exe文件）的绝对路径（本人以使用OpenFileDialog为例），命令AppContainer控件启动之。</p>
<div class="cnblogs_code">
<pre><span style="color: #008080;">1</span> appContainer1.AppFilename =<span style="color: #000000;"> openEXE.FileName;
</span><span style="color: #008080;">2</span> appContainer1.Start();</pre>
</div>
<p>这个AppContainer控件有什么好处呢？</p>
<p>1、原作者想到的Resize和随宿主程序关闭而关闭的问题，AppContainer都实现了。</p>
<p>2、AppContainer指定要嵌入的应用程序和启动是分开的，这样更灵活，开发过程中也不会看到如下的情况了：开发的时候原作者的控件就&ldquo;情不自禁&rdquo;地把内嵌程序加载进来了。</p>
<p>&nbsp;<a href="http://images.cnblogs.com/cnblogs_com/bitzhuwei/201205/201205242025297422.png"><img style="display: inline; border-width: 0px;" title="开发的时候原作者的控件就&ldquo;情不自禁&rdquo;地把内嵌程序加载进来了" src="http://images.cnblogs.com/cnblogs_com/bitzhuwei/201205/201205242025308751.png" alt="开发的时候原作者的控件就&ldquo;情不自禁&rdquo;地把内嵌程序加载进来了" width="862" height="619" border="0" /></a></p>
<p>3、AppContainer防范了各种可能出错的情形，例如禁止自己嵌入自己（死循环）、内嵌Console程序时提示不能嵌入、参数为null或无效的检验等。</p>
<p>4、其它。例如，AppContainer里面不会使用Thread.Sleep(1000);这样低端的句子来保证程序正确地嵌入（而且对于类似photoshop这样启动很慢的程序也保证不了），而是通过Application.Ilde事件实现了在被嵌程序加载完毕后才将其窗体嵌入的技巧。</p>
<p>当然，有些应用程序是不能这么自动化地嵌入进来的。因为程序启动窗体和主窗体句柄不一样，AppContainer无法获得主窗体句柄，所以无法自动嵌入。</p>
<p>为了解决这个问题，我在宿主窗体的状态栏上设置了&ldquo;句柄嵌入&rdquo;标签，点击&ldquo;句柄嵌入&rdquo;，你可以填入想嵌入的应用程序主窗体句柄，然后宿主窗体就可以嵌入它了。</p>
<p><a href="http://images.cnblogs.com/cnblogs_com/bitzhuwei/201205/201205242025326045.png"><img style="display: inline; border-width: 0px;" title="我在宿主窗体的状态栏上设置了&ldquo;句柄嵌入&rdquo;标签，点击&ldquo;句柄嵌入&rdquo;，你可以填入想嵌入的应用程序主窗体句柄，然后宿主窗体就可以嵌入它了" src="http://images.cnblogs.com/cnblogs_com/bitzhuwei/201205/201205242025344974.png" alt="我在宿主窗体的状态栏上设置了&ldquo;句柄嵌入&rdquo;标签，点击&ldquo;句柄嵌入&rdquo;，你可以填入想嵌入的应用程序主窗体句柄，然后宿主窗体就可以嵌入它了" width="420" height="373" border="0" /></a></p>
<p>然后有同学就问了，我怎么知道想要嵌入的窗体句柄是多少啊？方法很多啦，我这里也提供一个自己制作的小程序，大家可以在这里下载：<a title="WindowDetective(窗口侦探)0.20.rar" href="http://files.cnblogs.com/bitzhuwei/WindowDetective(%E7%AA%97%E5%8F%A3%E4%BE%A6%E6%8E%A2)0.20.rar" target="_blank">WindowDetective(窗口侦探)0.20.rar</a></p>
<p>界面是这个样子的：</p>
<p><a href="http://images.cnblogs.com/cnblogs_com/bitzhuwei/201205/201205242025352367.png"><img style="display: inline; border-width: 0px;" title="我的WindowDetective界面是这个样子的" src="http://images.cnblogs.com/cnblogs_com/bitzhuwei/201205/201205242025362584.png" alt="我的WindowDetective界面是这个样子的" width="304" height="164" border="0" /></a></p>
<p>里面&ldquo;句柄：{1903014}&rdquo;那一行就给出了本人正在用的Windows Live Writer的主窗体句柄。</p>
<p>用法很简单，启动这个程序后，它会自动检测鼠标所在位置的窗体信息，显示在窗口中。所以把鼠标放在你想了解的窗体菜单栏上就OK了。QQ TM版也可以这样嵌进来滴。（QQ嵌不进来，不知道腾讯在搞什么）</p>
<p><a href="http://images.cnblogs.com/cnblogs_com/bitzhuwei/201205/201205242025369170.png"><img style="display: inline; border-width: 0px;" title="QQ TM版也可以这样嵌进来" src="http://images.cnblogs.com/cnblogs_com/bitzhuwei/201205/20120524202537533.png" alt="QQ TM版也可以这样嵌进来" width="417" height="586" border="0" /></a></p>
<p>大家还可以试试把QQ对话框嵌进来，很好玩哦~</p>
<p>&nbsp;<a href="http://images.cnblogs.com/cnblogs_com/bitzhuwei/201205/201205242025372419.png"><img style="display: inline; border-width: 0px;" title="大家还可以试试把QQ对话框嵌进来，很好玩哦~" src="http://images.cnblogs.com/cnblogs_com/bitzhuwei/201205/201205242025384545.png" alt="大家还可以试试把QQ对话框嵌进来，很好玩哦~" width="521" height="541" border="0" /></a></p>
<p>&nbsp;</p>
