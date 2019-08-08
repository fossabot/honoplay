import React from 'react';
import { withStyles } from '@material-ui/core/styles';
import { Typography, CardContent, Card, ButtonBase, MuiThemeProvider } from '@material-ui/core';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { Style, theme } from './Style';

window.__MUI_USE_NEXT_TYPOGRAPHY_VARIANTS__ = true;

class CardButton extends React.Component {

  render() {
    const { classes, cardName, cardDescription, onClick, forTraining, iconName } = this.props;

    return (
      <MuiThemeProvider theme={theme}>
        <ButtonBase
          type="button"
          onClick={onClick}
          className={classes.centerCardButton}>
          <Card
            className={forTraining ? classes.cardButtonTraining : classes.cardButton}>
            <CardContent>
              <div
                className={classes.centerCardButton}>
                <FontAwesomeIcon
                  className={classes.iconCardButton}
                  icon={iconName}>
                </FontAwesomeIcon>
              </div>
              <Typography
                variant="h6"
                className={classes.typographyCardButton} >
                {cardName}
              </Typography>
              <Typography
                paragraph
                className={classes.paragraphCardButton}>
                {cardDescription}
              </Typography>
            </CardContent>
          </Card>
        </ButtonBase>
      </MuiThemeProvider>
    );
  }
}


export default withStyles(Style)(CardButton);