using UnityEngine;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif
public class LP_Clouds : MonoBehaviour {

	[Header("Clouds Limit Density:")]
	[Range(0.0f, 1.0f)]
	public float density_limit=1f;// Maximum allowed density (reduce for clouds with many vertices)

	[Header("Cloud Density:")]
	[Range(0.0f, 1.0f)]
	public float density = .3f;//density variable - change from editor

	[Header("Quality(Number of particles):")]
	[Range(1, 10)]
	public int quality = 10;// You need to decrease this value for mobile devices
	[Header("Resolution:")]
	public int height =128;//resolution 128x128 is fine for standalone and web applications, should be reduced for mobile
	public int width =128;
	private int depth =1;
	[Header("Clouds area scale:")]
	public float clouds_scale=20f;// main scaling parameter

	[Header("Clouds size scale:")]
	public float clouds_size=20f;// main scaling parameter

	[Header("Clouds size RND(0...1):")]
	[Range(0.0f, 1.0f)]
	public float clouds_size_RND=.5f;// size RND scaling parameter (0- no randomization, 1- max randomization)

	[Header("Clouds average lifetime:")]
	public float clouds_lifetime=6f;// main scaling parameter

	[Header("Clouds speed:")]
	[Range(0.0f, 10.0f)]
	public float speed_k_value=1f;

	[Header("Clouds brightness:")]
	[Range(0.0f, 1.0f)]
	public float brightness_max = 1f; // maximum cloud brightness

	[Header("Clouds Rotation RND:")]
	public Vector3 rot_RND; 

	[Header("Show GUI:")]

	public bool show_gui = true; // to show or not to show...gui =)
	

	[HideInInspector]
	public float speed_k;

	[HideInInspector]
	public float emission_extra = 0.5f;


	[HideInInspector]
	public float treshold;
	[HideInInspector]
	public float[,] noise ;
	[HideInInspector]
	public float particle_creation_k_value ;
	[HideInInspector]
	public float distance_covered=0f;
	[HideInInspector]
	public float lifetime_base = 6f;//basic lifetime


	private float xOrg;
	private float yOrg;
	private float brightness_max_old = 0f;
	//private float brightness_extra = 0.2f; // average brightness - is being changed by slider
	private float emission_extra_old = 0f;
	private int randomSeed_x;
	private int randomSeed_y;
	private float scale = 10F;//noise scale
	private float treshold_old;
	private Vector3[] Rand;
	private Color[] Colors;
	private float[] cls_size;
	private float delta_t=0f;
	private float old_t=0f;
	private float cloud_size_extra=1f;
	private float cloud_size_extra_old=1f;
	private Color[] pix;

	//private float day_speed=1f;// this variable is used to change clouds speed and lifetime together using in-game gui
	private float day_speed_old;
	void Start() {

		lifetime_base = clouds_lifetime;
		speed_k =speed_k_value*clouds_scale;
		Rand = new Vector3[width*height*depth];// create arrays to store random positions of the particles,there creation colors and sizes
		Colors = new Color[width*height*depth];//
		cls_size = new float[width*height*depth];//
		treshold = 1f - density;// treshold value is used to create a perlin noise based clouds map(0...1)
		treshold_old= treshold;
		particle_creation_k_value = density*10f;// just a koefficient
		this.GetComponent<ParticleSystem>().maxParticles = 1000000;
		this.GetComponent<ParticleSystem>().startRotation = Random.Range(0f,360f);// I don't think that it's really neccessary, but, well...
		noise = new float[width, height];
		randomSeed_x = Random.Range(1,1000);// random seed to randomize perlin noise
		randomSeed_y = Random.Range(1,1000);
		xOrg+=randomSeed_x;
		yOrg+=randomSeed_y;
		CalcSpeed();
		CalcNoise();//generating noise
		Createps();
		InvokeRepeating("UpdateOldps",.2f,.2f);//  updating already created particles as clouds move slowly, no need to do it often, for mobile dencrease this value

	}

	void OnGUI(){//--basic gui controls
	if (show_gui)
		{
			GUIStyle blStyle = new GUIStyle();
			blStyle.normal.textColor = Color.black;
			GUI.Label(new Rect(10,25,200,20),"Clouds Density:",blStyle);
			density = Mathf.CeilToInt(GUI.HorizontalSlider(new Rect(10,45,130,20),Mathf.CeilToInt(density*10f),1,density_limit*10))/10f;// Some math was added just to quantize the density
			//print(density);
			treshold = 1f - density;
			particle_creation_k_value = density*10f;
			if (treshold!=treshold_old){
				treshold_old=treshold;
				Createps();
			}
			GUI.Label(new Rect(150,25,200,20),"Clouds brightness:",blStyle);
			brightness_max =GUI.HorizontalSlider(new Rect(150,45,130,20),brightness_max,0f,1f);
			if (brightness_max!=brightness_max_old){
				brightness_max_old = brightness_max;
				ChangeColor();
			}
			GUI.Label(new Rect(300,25,200,20),"Out-scattering/absorbtion:",blStyle);
			emission_extra =GUI.HorizontalSlider(new Rect(300,45,130,20),emission_extra,0f,1f);
			if (emission_extra!=emission_extra_old){
				emission_extra_old = emission_extra;
				ChangeColor();
				//this.transform.FindChild("Shadow").gameObject.SendMessage("ChangeShadow_emission");
			}
			GUI.Label(new Rect(500,25,200,20),"Clouds Size:",blStyle);
			cloud_size_extra =GUI.HorizontalSlider(new Rect(450,45,130,20),Mathf.CeilToInt(cloud_size_extra*10f),5f,20f)/10f;
			cloud_size_extra = Mathf.CeilToInt(cloud_size_extra*10f)/10f;
			if (cloud_size_extra!=cloud_size_extra_old){
				ChangeSize();
				cloud_size_extra_old = cloud_size_extra;
			}
			// You can change clouds movement speed (for higher speeds increase invoke repeating frequency or just call UpdateOldps function inside Update instead)
			/*
			GUI.Label(new Rect(600,25,200,20),"Clouds Speed and lifetime:",blStyle);
			day_speed =Mathf.CeilToInt(GUI.HorizontalSlider(new Rect(600,45,130,20),Mathf.CeilToInt(day_speed),1f,10f));
			if (day_speed!=day_speed_old){
				day_speed_old = day_speed;
				CalcSpeed();
				Createps();
			}
			*/
		}
	}//---

	void CalcSpeed(){//calculating interconnected lifetime and speed

		//old approach, now can be changed by script or in editor only
		/*if (show_gui){ //if we control it using gui
			clouds_lifetime= lifetime_base/day_speed;

			speed_k_value = 0.5f*day_speed*day_speed;
			speed_k =speed_k_value*clouds_scale;
			so = new SerializedObject(this.GetComponent<ParticleSystem>());
			so.FindProperty("VelocityModule.z.scalar").floatValue = speed_k;
			so.ApplyModifiedProperties();
			print(speed_k);
			//ParticleSystem.VelocityOverLifetimeModule p_vel = this.GetComponent<ParticleSystem>().velocityOverLifetime;
			//ParticleSystem.MinMaxCurve rate = new ParticleSystem.MinMaxCurve();
			//rate.constantMax = speed_k;
			//p_vel.z = rate;
		} else{//if we use values from editor*/   
			speed_k =speed_k_value*clouds_scale;
			ParticleSystem.VelocityOverLifetimeModule p_vel = this.GetComponent<ParticleSystem>().velocityOverLifetime;
			ParticleSystem.MinMaxCurve rate = new ParticleSystem.MinMaxCurve();
			rate.constantMax = speed_k;
			p_vel.z = rate;
		//}
	}

//simple 2d perlin noise
	void CalcNoise() {
		float y = 0.0F;
		while (y < height) {
			float x = 0.0F;
			while (x < width) {

				
				float xCoord = (xOrg + x) / width;
				float yCoord = (yOrg + y) / height;

				float sample = Mathf.PerlinNoise(xCoord* scale, yCoord* scale)+Mathf.PerlinNoise(xCoord*2* scale, yCoord*2* scale)/2+Mathf.PerlinNoise(xCoord*4* scale, yCoord*4* scale)/4+Mathf.PerlinNoise(xCoord*8* scale, yCoord*8* scale)/8-.3f;

				noise[(int)x,(int)y] = sample;
				x++;
			}
			y++;
		}

	}

//this function is responsible for controlling particle colors
	void ChangeColor(){
		ParticleSystem.Particle[] particles=new ParticleSystem.Particle[this.GetComponent<ParticleSystem>().particleCount];

		int count = this.GetComponent<ParticleSystem>().GetParticles(particles);
		for (int i=0;i<count;i++){
			particles[i].startColor =  new Color(brightness_max,brightness_max,brightness_max, emission_extra) ;
		}
		this.GetComponent<ParticleSystem>().SetParticles(particles,count);
	}

	void ChangeSize(){
		ParticleSystem.Particle[] particles=new ParticleSystem.Particle[this.GetComponent<ParticleSystem>().particleCount];
		
		int count = this.GetComponent<ParticleSystem>().GetParticles(particles);

		for (int i=0;i<count;i++){
			particles[i].startSize*=cloud_size_extra/cloud_size_extra_old;

		}


		this.GetComponent<ParticleSystem>().SetParticles(particles,count);
	}

//This function is creating particles, is called when the particles should be created/recreated
	void Createps(){
		ParticleSystem.Particle[] particles=new ParticleSystem.Particle[width*height*depth];

		//int count = this.GetComponent<ParticleSystem>().GetParticles(particles);
		int p_i=0;
		for (int i=0;i < (width);i+=2){
			for (int j=0;j<(height);j+=2)
			{

					//Random.Range(-20+quality+Mathf.Ceil(particle_creation_k_value),10)
				if (noise[i,j]>=treshold && Random.Range(0,10*particle_creation_k_value/Mathf.Sqrt(quality))<5*density){//particle is created if noise level at this point is higher, than treshold,
					//second statement just ensures that the number of particles depends on a quality parameter and stays the same independently from the density.

					for (int d=0;d<depth;d++){



						//--- Here you can see math equations defining main particle parameteres
						float brightness =Mathf.Lerp(0f,brightness_max,(noise[i,j]-treshold)/(1f-treshold));
						float emission = 1f;//Mathf.Lerp(0.6f,Random.Range(0.6f,.1f),(noise[i,j]-treshold)/(1f-treshold));

						if (noise[i,j]>.6f){
							particles[p_i].startSize = Random.Range(1f-clouds_size_RND/2f,1f+clouds_size_RND/2f)*noise[i,j]*noise[i,j]*clouds_size*2f*Mathf.Pow(particle_creation_k_value,1/2.5f);
						} else {
							particles[p_i].startSize =Random.Range(1f-clouds_size_RND/2f,1f+clouds_size_RND/2f)*.36f*clouds_size*2f*Mathf.Pow(particle_creation_k_value,1/2.5f);
						}


						cls_size[p_i] = particles[p_i].startSize;
						Colors[p_i] = new Color(1f-brightness,1f-brightness,1f-brightness,emission*emission_extra);
						Rand[p_i] =Random.insideUnitSphere/2f;

						particles[p_i].startColor = Colors[p_i] ;

						float new_pos_z =(j-height/2f)*clouds_scale + distance_covered;
						if (new_pos_z>height*clouds_scale)
							new_pos_z -= height*clouds_scale;
						particles[p_i].position = this.transform.position + new Vector3((i-width/2f)*clouds_scale,(d-depth/2f+Random.Range(0f,1f))*((noise[i,j]-treshold)/(1f-treshold))*clouds_scale*2f,new_pos_z)+Rand[p_i];

						float life = (Random.Range(2f*clouds_lifetime/3f,4f*clouds_lifetime/3f));

						particles[p_i].startLifetime = life;
						particles[p_i].remainingLifetime = Mathf.Lerp(0.1f,particles[p_i].startLifetime,Random.Range(0f,1f));
						particles[p_i].randomSeed=(uint)Random.Range(0,6);
						// for some reason doesn't work in early versions of unity (works in 5.5 and late 5.4.x)
						particles[p_i].rotation3D = new Vector3(Random.Range(-rot_RND.x,rot_RND.x),Random.Range(-rot_RND.y,rot_RND.y),Random.Range(-rot_RND.z,rot_RND.z));//Vector3.Lerp(-rot_RND,rot_RND,Random.Range(0f,1f));//new Vector3(Random.Range(-30f,30f),Random.Range(-90f,90f),Random.Range(-30f,30f));
						//---

						p_i++;
					}

				}

			}

	}
		//this.transform.FindChild("Shadow").gameObject.SendMessage("SetNoise");// we provide info to shadows creating script
		//this.transform.FindChild("Shadow").gameObject.SendMessage("SetSpeed");// we provide info to shadows creating script

		this.GetComponent<ParticleSystem>().SetParticles(particles,p_i);//applying all particle changes
		ChangeColor();
		ChangeSize();

	}

	void UpdateOldps(){
		ParticleSystem.Particle[] particles=new ParticleSystem.Particle[this.GetComponent<ParticleSystem>().particleCount];
		delta_t = Time.time-old_t;
		old_t = Time.time;//checking how much time passed since last movement(to be sure)
		int count = this.GetComponent<ParticleSystem>().GetParticles(particles);
		distance_covered+=speed_k*delta_t;

		if (distance_covered>=height*clouds_scale)
			distance_covered-=height*clouds_scale;
		//this.transform.FindChild("Shadow").gameObject.SendMessage("SetDistance");
		for (int i=0;i<count;i++){
			//particles[i].position+=new Vector3(0f,0f,speed_k*delta_t); // no need for manual movement, done using PS velocity
			//if particle is dying we check if it's crossed the border already, if yes - "recreating" it on the other side, else at the same position
			if (particles[i].remainingLifetime<particles[i].startLifetime*.05f){
				particles[i].remainingLifetime=particles[i].startLifetime;


			}
			if(particles[i].position.z>this.transform.position.z+height*clouds_scale/2f){
				float z_delta = particles[i].position.z - (this.transform.position.z+height*clouds_scale/2f);
				particles[i].position = new Vector3(particles[i].position.x,particles[i].position.y,this.transform.position.z-height*clouds_scale/2f+z_delta);
				particles[i].remainingLifetime=particles[i].startLifetime;
			}

		

		
		}
		this.GetComponent<ParticleSystem>().SetParticles(particles,count);
	}



}
