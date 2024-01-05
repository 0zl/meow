using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grimoire.Tools.Maid
{
    public static class ClassPreset
    {
        public static void LR()
        {
            // skill list
            UI.Maid.MaidRemake.Instance.tbSkillList.Text = "3,1,2,4";

			// heal skill
			UI.Maid.MaidRemake.Instance.tbHealSkill.Text = String.Empty;
            UI.Maid.MaidRemake.Instance.numHealthPercent.Value = 60;

			// buff skill
			UI.Maid.MaidRemake.Instance.tbBuffSkill.Text = "3";

            // additional settings
            UI.Maid.MaidRemake.Instance.cbWaitSkill.Checked = false;
        }

        public static void LC()
        {
			// skill list
			UI.Maid.MaidRemake.Instance.tbSkillList.Text = "1,2,4";

            // heal skill
            UI.Maid.MaidRemake.Instance.tbHealSkill.Text = "3";
			UI.Maid.MaidRemake.Instance.numHealthPercent.Value = 60;

			// buff skill
			UI.Maid.MaidRemake.Instance.tbBuffSkill.Text = String.Empty;

            // additional settings
            UI.Maid.MaidRemake.Instance.cbWaitSkill.Checked = false;
        }

        public static void LOO()
        {
            // skill list
            UI.Maid.MaidRemake.Instance.tbSkillList.Text = "2,1,3,4";

            // heal skill
            UI.Maid.MaidRemake.Instance.tbHealSkill.Text = String.Empty;
            UI.Maid.MaidRemake.Instance.numHealthPercent.Value = 60;

            // buff skill
            UI.Maid.MaidRemake.Instance.tbBuffSkill.Text = "1,2,3";

            // additional settings
            UI.Maid.MaidRemake.Instance.cbWaitSkill.Checked = false;
        }

        public static void SC()
        {
            // skill list
            UI.Maid.MaidRemake.Instance.tbSkillList.Text = "3,1,2,4";

            // heal skill
            UI.Maid.MaidRemake.Instance.tbHealSkill.Text = String.Empty;
            UI.Maid.MaidRemake.Instance.numHealthPercent.Value = 60;

            // buff skill
            UI.Maid.MaidRemake.Instance.tbBuffSkill.Text = "2,3";

            // additional settings
            UI.Maid.MaidRemake.Instance.cbWaitSkill.Checked = false;
        }

        public static void AP()
        {
            // skill list
            UI.Maid.MaidRemake.Instance.tbSkillList.Text = "1,2,3";

            // heal skill
            UI.Maid.MaidRemake.Instance.tbHealSkill.Text = String.Empty;
            UI.Maid.MaidRemake.Instance.numHealthPercent.Value = 60;

            // buff skill
            UI.Maid.MaidRemake.Instance.tbBuffSkill.Text = String.Empty;

            // additional settings
            UI.Maid.MaidRemake.Instance.cbWaitSkill.Checked = false;
        }

        public static void CCMD()
        {
            // skill list
            UI.Maid.MaidRemake.Instance.tbSkillList.Text = "1,1,1,1,3";

            // heal skill
            UI.Maid.MaidRemake.Instance.tbHealSkill.Text = "2";
            UI.Maid.MaidRemake.Instance.numHealthPercent.Value = 75;

            // buff skill
            UI.Maid.MaidRemake.Instance.tbBuffSkill.Text = String.Empty;

            // additional settings
            UI.Maid.MaidRemake.Instance.cbWaitSkill.Checked = true;
        }

        public static void SSOT()
        {
            // skill list
            UI.Maid.MaidRemake.Instance.tbSkillList.Text = "4,2,3,1,2";

            // heal skill
            UI.Maid.MaidRemake.Instance.tbHealSkill.Text = String.Empty;
            UI.Maid.MaidRemake.Instance.numHealthPercent.Value = 60;

            // buff skill
            UI.Maid.MaidRemake.Instance.tbBuffSkill.Text = String.Empty;

            // additional settings
            UI.Maid.MaidRemake.Instance.cbWaitSkill.Checked = true;
        }

        public static void NCM()
        {
            // skill list
            UI.Maid.MaidRemake.Instance.tbSkillList.Text = "1,2,4,1,2,1,3";

            // heal skill
            UI.Maid.MaidRemake.Instance.tbHealSkill.Text = String.Empty;
            UI.Maid.MaidRemake.Instance.numHealthPercent.Value = 60;

            // buff skill
            UI.Maid.MaidRemake.Instance.tbBuffSkill.Text = String.Empty;

            // additional settings
            UI.Maid.MaidRemake.Instance.cbWaitSkill.Checked = true;
        }

        public static void TK()
        {
            // skill list
            UI.Maid.MaidRemake.Instance.tbSkillList.Text = "2,1,1,1,2,1,4,3,1";

            // heal skill
            UI.Maid.MaidRemake.Instance.tbHealSkill.Text = String.Empty;
            UI.Maid.MaidRemake.Instance.numHealthPercent.Value = 60;

            // buff skill
            UI.Maid.MaidRemake.Instance.tbBuffSkill.Text = String.Empty;

            // additional settings
            UI.Maid.MaidRemake.Instance.cbWaitSkill.Checked = true;
        }

        // for making sure the setting is applied

        public static void cbClear()
        {
            UI.Maid.MaidRemake.Instance.cbUseHeal.Checked = false;
            UI.Maid.MaidRemake.Instance.cbBuffIfStop.Checked = false;
        }

        public static void cbSet()
        {
            UI.Maid.MaidRemake.Instance.cbUseHeal.Checked = true;
            UI.Maid.MaidRemake.Instance.cbBuffIfStop.Checked = true;
        } 
    }
}
