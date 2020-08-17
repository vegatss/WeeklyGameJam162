using UnityEngine;
using System.Collections;
using UnityEngine.Tilemaps;

public class SeaAnimation : MonoBehaviour {

	public Tile[] seaSprites;
	public Tile[] seaEdgeSprites;
    public Tilemap seaTilemap;

	private readonly float animationSpeed = 0.12f;
    private readonly float edgeAnimationSpeed = 0.5f;
    private readonly int horizontal = 15;
    private readonly int verticalMin = 4;
    private readonly int verticalMax = 8;
    private readonly int edgeVertical = 3;
    private int spriteIndex = 0;
    private int edgeSpriteIndex = 0;

    private void Start() {
        StartCoroutine(AnimateSea());
        StartCoroutine(AnimateEdges());
    }
    private IEnumerator AnimateSea() {        
        for(int i = -horizontal; i < horizontal; i++) {
            for(int j = verticalMin; j < verticalMax; j++) {
                seaTilemap.SetTile(new Vector3Int(i, j, 0), seaSprites[spriteIndex]);
                
            }
        }
        spriteIndex++;
        if(spriteIndex >= seaSprites.Length) {
            spriteIndex = 0;
        }
        yield return new WaitForSeconds(animationSpeed);
        StartCoroutine(AnimateSea());
    }
    private IEnumerator AnimateEdges() {
        for(int i = -horizontal; i < horizontal; i++) {
            seaTilemap.SetTile(new Vector3Int(i, edgeVertical, 0), seaEdgeSprites[edgeSpriteIndex]);
        }
        edgeSpriteIndex = edgeSpriteIndex == 0 ? 1 : 0;
        yield return new WaitForSeconds(edgeAnimationSpeed);
        StartCoroutine(AnimateEdges());
    }
}
