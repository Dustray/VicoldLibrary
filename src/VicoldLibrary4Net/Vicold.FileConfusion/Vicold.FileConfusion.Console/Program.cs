
using Vicold.FileConfusion;

System.Console.WriteLine("Hello World!");
var fcb = new FcBus();
//fcb.Confuse(@"O:\VID_20200617_175306.mp4", @"O:\VID_20200617_175306_ov.mp4");
//fcb.Confuse(@"O:\Vid20200616223151259-1.m4v", @"O:\Vid20200616223151259-111.m4v");
fcb.AntiConfuse(@"O:\VID_20200617_175306_ov.mp4", @"O:\VID_20200617_175306_ov2.mp4");