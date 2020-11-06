using UnityEngine;

public class moveLastResult : MonoBehaviour {
    Vector2 posInicial;
    Vector2 posFinal;
    private void Start() {
        //posInicial = new Vector2(-188,123);
        posFinal = new Vector3(-188,31,0);
    }
    private void Update() {
        this.transform.localPosition = Vector3.MoveTowards(this.transform.localPosition,posFinal,Time.deltaTime*60f);

        
    }

}