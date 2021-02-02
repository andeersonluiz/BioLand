

using UnityEngine;


public class moveCloud : MonoBehaviour
{
    private Parameters parameters;
    private Enviroment ev;
    private CloudScript cloudScript;

    void Start()
    {
        ev = FindObjectOfType<Enviroment>();
        cloudScript = FindObjectOfType<CloudScript>();
        parameters = new Parameters();
    }
    private void Update()
    {
        if (!ev.isPaused)
        {
            gameObject.transform.Translate(Vector3.left * cloudScript.speedCloud * Time.deltaTime);
            if (gameObject.transform.position.x <= -14f)
            {
                Destroy(gameObject);
            }
        }
    }
    public float speedCloud { get; set; }

}