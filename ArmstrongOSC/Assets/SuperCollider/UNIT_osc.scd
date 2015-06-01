OSCFunc
57120
n = NetAddr("127.0.0.1", NetAddr.langPort); // local machine

// Llevar registro de quienes están sonando
~osc = Array.new(5000);
~ultimoInd=0;
(
~ultimoInd=0;
a=OSCdef('mX',{|x|
	var mag;
	//"x ".post;
	x.postln;
},"/mX");
b=OSCdef('mY',{|x|
	var mag;
	//"y ".post;
	//x.postln;
	~osc.add(x);
},"/mY");
c=OSCdef('aX',{|x|
	var mag;
	"ax ".post;
	x.postln;
},"/aX");
d=OSCdef('aY',{|x|
	var mag;
	"ay ".post;
	x.postln;
},"/aY");
e=OSCdef('aZ',{|x|
	var mag;
	"az ".post;
	x.postln;
},"/aZ");
f=OSCdef('ID',{|x|
	var mag;
	"ID ".post;
	x.postln;
},"/ID");
)


~sin={SinOsc.ar()}
m = NetAddr("127.0.0.1", 6666); // loopback

(
10000.do({
m.sendMsg("/chat", "Hello App 1");
});
)

(
a=Routine({
	var num;
	1000.do({

	num= 127.rand.postln;
		m.sendMsg("/chat", num);
		(0.1).wait;
"mandar".postln;

	});
});
)
a.play
a.stop