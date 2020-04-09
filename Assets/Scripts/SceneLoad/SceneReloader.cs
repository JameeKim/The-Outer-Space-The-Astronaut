using UnityEngine;
using UnityEngine.SceneManagement;

namespace SceneLoad {
    [DisallowMultipleComponent]
    public class SceneReloader : MonoBehaviour
    {
        public void Reload()
        {
            SceneManager.LoadScene(0, LoadSceneMode.Single);
        }
    }
}
