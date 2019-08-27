import React, { Children } from 'react';
import { withStyles } from '@material-ui/core/styles';
import {
  ExpansionPanel,
  ExpansionPanelDetails,
  ExpansionPanelSummary,
  Typography,
  Divider
} from '@material-ui/core';
import ExpandMoreIcon from '@material-ui/icons/ExpandMore';
import Style from './Style';


class ControlledExpansionPanels extends React.Component {

  expanded = null;

  render() {
    const { classes, open, children, panelDetails } = this.props;
    open ? this.expanded = true : this.expanded = false

    return (
      <div className={classes.root}>
        <ExpansionPanel expanded={this.expanded}>
          <ExpansionPanelSummary expandIcon={<ExpandMoreIcon />}>
          </ExpansionPanelSummary>
          <ExpansionPanelDetails>
            <Typography  variant="h6" component="h2">
              {panelDetails}
            </Typography>
          </ExpansionPanelDetails>
          <Divider />
          {children}
        </ExpansionPanel>

      </div>
    );
  }
}

export default withStyles(Style)(ControlledExpansionPanels);
