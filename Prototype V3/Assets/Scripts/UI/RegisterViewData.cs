
public class RegisterViewData {
    private IViewUI view;
    private int index;

    public IViewUI View { get { return view; } }
    public int Index { get { return index; } }

    public RegisterViewData(IViewUI view, int index) {
        this.view = view;
        this.index = index;
    }
}
