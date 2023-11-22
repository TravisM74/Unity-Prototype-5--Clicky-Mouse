using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using Unity.VisualScripting;
using UnityEngine;


public class Target : MonoBehaviour
{
    private Rigidbody targetRb;
    private GameManager gameManager;
    private float minSpeed = 9;
    private float maxSpeed = 14;
    private float maxTorque = 10;
    private float xRange = 4;
    private float yPosition = -1.3f;
    
    public ParticleSystem explosionParticle;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        targetRb = GetComponent<Rigidbody>();
        targetRb.AddForce(RandomForceUp(), ForceMode.Impulse);
        targetRb.AddTorque(AddRandomTorque(), AddRandomTorque(), AddRandomTorque(), ForceMode.Impulse);
        transform.position = RandomXPosition();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown(){
        if (gameManager.isGameActive){
            if (gameObject.name == "Bad1(Clone)"){
                gameManager.UpdateScore(-15);
            }
            if (gameObject.name == "Good1(Clone)"){
                gameManager.UpdateScore(5);
            }
            if (gameObject.name == "Good2(Clone)"){
                gameManager.UpdateScore(Random.Range(5,10));
            }
            if (gameObject.name == "Good3(Clone)"){
                gameManager.UpdateScore(15);
            }
            //Debug.Log(gameObject.name);
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider col){
        Destroy(gameObject);

        if (!gameObject.CompareTag("Bad")){
            int newLives = gameManager.lives -1;
            gameManager.UpdateLives(newLives);
            if (gameManager.lives < 1){
                gameManager.GameOver();

            }
        }
    }
    private Vector3 RandomForceUp(){
        return Vector3.up * Random.Range(minSpeed, maxSpeed);
    }
    private float AddRandomTorque(){
        return Random.Range(-maxTorque,maxTorque);
    }
    private Vector3 RandomXPosition(){
        return new Vector3(Random.Range(-xRange,xRange), yPosition, 0);
    }
   
}
