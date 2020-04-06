using UnityEngine;

namespace Dialog {
    public class DialogPlayer : MonoBehaviour
    {
        public DialogSeries tmpDialogList;
        public DialogPresenter dialogPresenter;

        public void PlayDialog()
        {
            dialogPresenter.gameObject.SetActive(true);
            dialogPresenter.StartDialog(tmpDialogList);
        }
    }
}
