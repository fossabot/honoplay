import React from 'react';
import { Button, InputBase, Grid, InputLabel } from '@material-ui/core';
import { withStyles } from '@material-ui/core/styles';
import { translate } from '@omegabigdata/terasu-api-proxy';
import { Style } from './Style';

class ImageInputComponent extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      filename: '',
      byteArray: null
    };
  }
  handleImageName = e => {
    this.setState({
      filename: e.target.value.replace('C:\\fakepath\\', '')
    });
    this.getBase64(e.target.files[0]);
  };

  getBase64 = file => {
    if (!file) {
      return;
    }
    const reader = new FileReader();
    reader.readAsArrayBuffer(file);

    reader.onload = e => {
      const arrayBuffer = e.target.result,
        array = Array.from(new Uint8Array(arrayBuffer));
      this.props.selectedImage(array);
      this.setState({ byteArray: array });
    };
  };

  render() {
    const { classes, labelName, error } = this.props;

    return (
      <div className={classes.inputRoot}>
        <Grid container spacing={3}>
          {labelName && (
            <Grid item xs={12} sm={2} className={classes.labelCenter}>
              <InputLabel className={classes.bootstrapFormLabel}>
                {labelName}
              </InputLabel>
            </Grid>
          )}
          <Grid item xs={8} sm={9}>
            <InputBase
              classes={{
                root: classes.bootstrapRoot,
                input: error ? classes.bootstrapError : classes.bootstrapInput
              }}
              value={this.state.filename}
              fullWidth
              error={error ? true : false}
            />
          </Grid>
          <Grid item xs={4} sm={1}>
            <input
              accept="image/*"
              className={classes.fileInput}
              id="button-file"
              multiple
              type="file"
              onChange={this.handleImageName}
            />
            <label htmlFor="button-file">
              <Button
                variant="outlined"
                component="span"
                className={classes.fileInputButton}
              >
                {translate('Browse')}
              </Button>
            </label>
          </Grid>
        </Grid>
      </div>
    );
  }
}

export default withStyles(Style)(ImageInputComponent);
