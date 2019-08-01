import React from 'react';
import { withStyles } from '@material-ui/core/styles';
import { Typography, CardContent, Card, ButtonBase, MuiThemeProvider } from '@material-ui/core';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { Style, theme } from './Style';

class CardButton extends React.Component {

  render() {
    const { classes, cardName, cardDescription, onClick, forTraining } = this.props;

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
                  icon="graduation-cap">
                </FontAwesomeIcon>
              </div>
              <Typography
                variant="h6"
                className={classes.typographyCardButton} >
                {cardName}
              </Typography>
              <Typography
                paragraph={true}
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