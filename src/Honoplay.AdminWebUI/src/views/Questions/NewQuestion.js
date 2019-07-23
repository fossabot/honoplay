import React from 'react';
import { translate } from '@omegabigdata/terasu-api-proxy';
import { withStyles } from '@material-ui/core/styles';
import { Grid, Divider, TextField } from '@material-ui/core';
import Style from '../Style';
import Typography from '../../components/Typography/TypographyComponent';
import Input from '../../components/Input/InputTextComponent';
import Button from '../../components/Button/ButtonComponent';

import Options from '../../components/Options/OptionsComponent';

class NewQuestion extends React.Component {

  constructor(props) {
    super(props);
    this.state = {
      answerData: [
        { order: 1, answer: "Tasarım Departmanı" },
        { order: 2, answer: "Yazılım Departmanı" },
        { order: 3, answer: "İnsan Kaynakları Departmanı" },
      ],
    }
  }

  render() {
    const { classes } = this.props;
    return (
      <div className={classes.root}>
        <Grid container spacing={24}>
          <Grid item xs={12} sm={12}>
            <Typography
              pageHeader={translate('CreateAQuestion')}
            />
          </Grid>
          <Grid item xs={12} sm={12} />
          <Grid item xs={12} sm={12} />
          <Grid item xs={12} sm={12}>
            <Input
              labelName={translate('QuestionText')}
              inputType="text"
            />
            <Input
              labelName={translate('Duration')}
              inputType="text"
            />
          </Grid>
          <Grid item xs={12} sm={11} />
          <Grid item xs={12} sm={1} >
            <Button
              buttonColor="secondary"
              buttonName={translate('Add')}
            />
          </Grid>
          <Grid item xs={12} sm={12}> <Divider /> </Grid>
          <Grid item xs={12} sm={12}>
            <Typography
              pageHeader={translate('Options')}
            />
          </Grid>
          <Grid item xs={12} sm={12} />
          <Grid item xs={12} sm={12} />
          <Grid item xs={12} sm={8} >
            <Input
              labelName={translate('Answer')}
              inputType="text"
            />
          </Grid>
          <Grid item xs={12} sm={3} >
            <Input
              labelName={translate('Order')}
              inputType="text"
            />
          </Grid>
          <Grid item xs={12} sm={1} >
            <Button
              buttonColor="secondary"
              buttonName={translate('Add')}
            />
          </Grid>
          <Grid item xs={12} sm={12} />
          <Grid item xs={12} sm={12} />
          <Grid item xs={12} sm={12}>
            <TextField
              disabled
              fullWidth
              multiline
              margin="normal"
              variant="outlined"
              value={"Türkiyede en çok kıskanılan takımın Fenerbahçe olmasının asıl sebebi nedir?"}
            />
          </Grid>
          <Grid item xs={12} sm={12}> <Divider /> </Grid>
          <Grid item xs={12} sm={12}>
            <Options
              data={this.state.answerData}
            />
          </Grid>
          <Grid item xs={12} sm={11} />
          <Grid item xs={12} sm={1} >
            <Button
              buttonColor="primary"
              buttonName={translate('Save')}
            />
          </Grid>
          <Grid item xs={12} sm={12}> <Divider /> </Grid>
        </Grid>
      </div>
    );
  }
}

export default withStyles(Style)(NewQuestion);