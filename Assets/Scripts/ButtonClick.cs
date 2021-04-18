using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ButtonClick : MonoBehaviour
{
	int i;
	GameObject newScene, characters, audioSource;
	ArrayList ids;
	GameObject wrong, winScreen;
	string[] bukvi = {"А", "Б","В", "Г", "Д", "Ѓ", "Е", "Ж", "З", "Ѕ", "И", "Ј",
	"К", "Л", "Љ", "М", "Н", "Њ", "О", "П", "Р", "С", "Т", "Ќ", "У", "Ф", "Х", "Ц", "Ч", "Џ", "Ш"};
	private void Start()
	{
		//Initialize starting elements
		this.wrong = GameObject.Find("wrongAnswerTxt");
		wrong.SetActive(false);		
		this.newScene = GameObject.Find("NewScene");
		newScene.SetActive(false);
		this.winScreen = GameObject.Find("WinScreen");
		winScreen.SetActive(false);
		ids = new ArrayList();
		this.characters = GameObject.Find("Characters");
		
	}
	public void restart()
	{
		winScreen.SetActive(false);
		characters.SetActive(true);
		wrong.SetActive(false);
		ids = new ArrayList();
		
	}
	public void exitGame()
	{
		Application.Quit();
	}
	void GetRandom()
	{
		for(int t=1;t<4;t++)
		{
			int rand = Random.Range(0, 31);
			while (ids.Contains(rand))
				rand = Random.Range(0, 31);
			ids.Add(rand);
		}
	}
	public void correctAnswer()
	{
		newScene.SetActive(false);
		winScreen.SetActive(true);
		wrong.SetActive(false);
	}
	public void wrongAnswer()
	{
		wrong.SetActive(true);
	}
	IEnumerator ExampleCoroutine()
	{	
		characters.SetActive(false);
		yield return new WaitForSeconds(1.5f);
		newScene.SetActive(true);

		//Audio
		AudioSource audioSource = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioSource>();
		AudioClip clip1 = Resources.Load("Sounds\\" + i.ToString(), typeof(AudioClip)) as AudioClip;
		audioSource.clip = clip1;

		//Image
		Image image = GameObject.FindGameObjectWithTag("Image").GetComponent<Image>();
		Sprite sprite = Resources.Load<Sprite>("Sprites\\" + i.ToString());
		image.sprite = sprite;

		//RANDOMIZE CHARACTERS & POSITIONS
		//Save the four avaliable positions
		Vector2[] vektori = { GameObject.Find("Char1").transform.position, GameObject.Find("Char2").transform.position,
			GameObject.Find("Char3").transform.position, GameObject.Find("Char4").transform.position };

		//Get 3 random & 1 selected character
		ids.Add(i); GetRandom();
		GameObject.Find("Char1").GetComponentInChildren<Text>().text = bukvi[(int)ids[0]];
		GameObject.Find("Char2").GetComponentInChildren<Text>().text = bukvi[(int)ids[1]];
		GameObject.Find("Char3").GetComponentInChildren<Text>().text = bukvi[(int)ids[2]];
		GameObject.Find("Char4").GetComponentInChildren<Text>().text = bukvi[(int)ids[3]];

		//Shuffle Vector positions array
		for (int t = 0; t < vektori.Length; t++)
		{
			Vector2 tmp = vektori[t];
			int r = Random.Range(t, vektori.Length);
			vektori[t] = vektori[r];
			vektori[r] = tmp;
		}

		//Assign each character a random position
		GameObject.Find("Char1").transform.position = vektori[0];
		GameObject.Find("Char2").transform.position = vektori[1];
		GameObject.Find("Char3").transform.position = vektori[2];
		GameObject.Find("Char4").transform.position = vektori[3];
	}

	void playAnimation ()
	{		
		//Animation & Starting the Coroutine
		GameObject.Find("animImage").SetActive(true);
		Animator anim = GameObject.Find("animImage").GetComponent<Animator>();
		anim.Play("fade");
		StartCoroutine(ExampleCoroutine());
	}

	public void TaskOnClick(int i)
	{
		this.i = i;
		playAnimation();
	}
}