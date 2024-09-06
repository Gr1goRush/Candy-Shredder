using System.Threading;
using System.Threading.Tasks;

public class BlocksCandyController
{
    private BlocksCandyView _blocksCandyView;
    private CancellationTokenSource _tokenSource;

    public BlocksCandyController(BlocksCandyView blocksCandyView)
    {
        _blocksCandyView = blocksCandyView;
        _tokenSource = new CancellationTokenSource();
    }

    public void Initialize()
    {
        SortCandyLine();
        ShowCandyLineStart();
        BaseUpdate();
    }

    public void BaseUpdate()
    {
        UpdateCandyLine();
    }

    public async void UpdateCandyLine()
    {
        try
        {
            TryShowCandyLine();
            await Task.Delay(_blocksCandyView.IncrementPerSecond != -1 ? (int)(_blocksCandyView.IncrementPerSecond * 1000) : -1, _tokenSource.Token);
            BaseUpdate();
        }
        catch (TaskCanceledException exception)
        {

        }
    }

    public void OnResumeGame()
    {
        _tokenSource.Cancel();
        _tokenSource = new CancellationTokenSource();
    }

    private void ShowCandyLineStart()
    {
        for (int i = 0; i < _blocksCandyView.CountStart; i++)
            TryShowCandyLine();
    }

    private void TryShowCandyLine()
    {
        foreach (var blockCandyLine in _blocksCandyView.ListCandyLine)
        {
            for (int i = 0; i < _blocksCandyView.Increment; i++)
            {
                var indexCandyLine = blockCandyLine.GetIndexFreeCandyLine();

                if (indexCandyLine >= blockCandyLine.CandyLines.Count)
                    return;

                foreach (var candy in blockCandyLine.CandyLines[indexCandyLine].Candies)
                {
                    candy.SetImage(_blocksCandyView.GetRandomCandyImage());
                    candy.SetActive(true);
                }
            }
        }
    }

    private void SortCandyLine()
    {
        foreach (var blockCandyLine in _blocksCandyView.ListCandyLine)
        {
            if (blockCandyLine.CandyLines.Count == 0)
                return;

            blockCandyLine.CandyLines.Sort(
                (CandyLineView candyLine1, CandyLineView candyLine2) =>
                {
                    return candyLine1.NumberLine.CompareTo(candyLine2.NumberLine);
                });
        }
    }
}
