using bb.bbInject;
using bb.Ui.View.Home;
using bb.UiModule;
using UnityEngine;

public class Test : MonoBehaviour
{
    private ViewService _viewService;

    [Inject]
    private void Inject(ViewService viewService)
    {
        _viewService = viewService;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _viewService.Show<HomeView, HomeViewController>(null);
        }
    }
}