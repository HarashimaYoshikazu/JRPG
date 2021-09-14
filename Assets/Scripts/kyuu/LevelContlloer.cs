using UnityEngine;

/// <summary>
/// プレイヤーのパラメーターを管理するコンポーネント
/// </summary>
public class LevelContlloer : MonoBehaviour
{
    /// <summary>レベルアップテーブルを読み込むため</summary>
    [SerializeField] LevelManager m_levelManager = default;
    /// <summary>プレイヤーのレベル</summary>
    //int m_level = 1;
    /// <summary>プレイヤーのパラメーター</summary>
    PlayerStats m_playerStats = default;


    public PlayerStats stat
    {
        get
        {
            return m_playerStats;
        }
        set
        {
            m_playerStats = value;
        }
    }


    public int level
    {
        get
        {
            return stat.Level;
        }
        private set
        {
            if (value > 0)
            {
                m_playerStats = m_levelManager.GetData(value);
            }

        }
    }



    void Start()
    {
        this.level = 1;
        // ReloadData();
    }

    /// <summary>
    /// レベルに対してプレイヤーのパラメーターを読み込み直す
    /// </summary>
    public void ReloadData()
    {
        PlayerStats stats = m_levelManager.GetData(this.level);

        if (stats.Level != 0)
        {
            m_playerStats = m_levelManager.GetData(this.level);
        }
    }

    /// <summary>
    /// レベルアップする
    /// </summary>
    /// <param name="level">レベルアップさせたいレベル数</param>
    public void LevelUp(int level = 1)
    {
        PlayerStats stats = m_levelManager.GetData(this.level + level);

        if (stats.Level != 0)
        {
            this.level += level;
            m_playerStats = m_levelManager.GetData(this.level);
        }
    }
}
